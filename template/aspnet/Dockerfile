# syntax=docker/dockerfile:1.3
FROM --platform=${TARGETPLATFORM:-linux/amd64} ghcr.io/openfaas/of-watchdog:0.9.2 as watchdog

FROM --platform=${TARGETPLATFORM:-linux/amd64} mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim as builder

# suppress data collection
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

# caches restore result by copying csproj file separately
WORKDIR /source/faas/function
COPY function/function.csproj .

# Copies the rest of the code
WORKDIR /source/faas
COPY . .

# build and publish
RUN dotnet publish \
    -c release \
    -r linux-x64 \
    --no-self-contained \
    -p:PublishReadyToRun=true \
    -o published \
    function/function.csproj

# runner
FROM --platform=${TARGETPLATFORM:-linux/amd64} mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim

COPY --from=watchdog /fwatchdog /usr/bin/

# Create a non-root user
RUN addgroup --system app \
    && adduser --system --ingroup app app

WORKDIR /home/app/
COPY --from=builder /source/faas/published .
RUN chown app:app -R /home/app

USER app

ENV fprocess="./function"
ENV cgi_headers="true"
ENV mode="http"
ENV upstream_url="http://localhost:9000"

ENV exec_timeout="30s"
ENV write_timeout="15s"
ENV read_timeout="15s"

ENV prefix_logs="false"

EXPOSE 8080

HEALTHCHECK --interval=3s CMD [ -e /tmp/.lock ] || exit 1

CMD ["fwatchdog"]
