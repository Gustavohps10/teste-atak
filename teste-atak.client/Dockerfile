FROM node:18 AS build

WORKDIR /app

COPY teste-atak.client/package*.json ./
RUN npm install

COPY teste-atak.client/ ./
RUN npm run build

FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]