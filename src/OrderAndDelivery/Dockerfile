FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci --production=false
COPY . .
RUN npm run build || true

FROM node:18-alpine AS runtime
WORKDIR /app
COPY --from=build /app .
ENV NODE_ENV=production
RUN npm prune --production || true
CMD ["node", "dist/index.js"]