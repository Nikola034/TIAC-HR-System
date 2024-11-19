# Use the official Node.js image as the base image
FROM node:latest AS build

# Set the working directory to the application's directory
WORKDIR /app

# Copy package.json and package-lock.json (if present)
COPY ./hr-system/package*.json ./

# Install application dependencies
RUN npm install

# Copy the rest of the application code
COPY ./hr-system .

# Build the Angular application
RUN npm run build:production

# Use a lightweight image for the production build
FROM nginx:alpine

RUN rm /etc/nginx/conf.d/default.conf

# Copy the built Angular application from the build image
COPY --from=build /app/dist/hr-system/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/

# Expose the default port for the Angular application
EXPOSE 80

# Start Nginx to serve the application
CMD ["nginx", "-g", "daemon off;"]
