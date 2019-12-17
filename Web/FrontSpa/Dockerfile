FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM node:latest AS node-build
WORKDIR /app

COPY ["Web/FrontSpa/ClientApp/", "/app"]
RUN npm install
RUN npm run build --prod

FROM nginx:stable-alpine as final

COPY ["Web/FrontSpa/default.conf", "/etc/nginx/conf.d/"]

RUN rm -rf /usr/share/nginx/html/*
COPY --from=node-build /app/dist/FrontOneTouch /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]