# React + Docker + Docker Hub Homework

This project is a demonstration of containerizing a modern React application using Vite, Docker, and Nginx.

## Features
- **React + TypeScript + Vite**: Modern frontend stack.
- **Premium UI**: Custom CSS with glassmorphism and smooth animations.
- **Multi-stage Docker Build**: Optimized production image serving via Nginx.
- **Containerized**: Fully portable and ready for any environment.

## Getting Started

### Local Development
1. Install dependencies:
   ```bash
   npm install
   ```
2. Run development server:
   ```bash
   npm run dev
   ```

### Docker
1. Build the image:
   ```bash
   docker build -t react-docker-hw .
   ```
2. Run the container:
   ```bash
   docker run -p 8080:80 react-docker-hw
   ```
3. Access the app at `http://localhost:8080`.

## Publication to Docker Hub

1. Tag the image:
   ```bash
   docker tag react-docker-hw <your-username>/react-docker-hw:latest
   ```
2. Push to Docker Hub:
   ```bash
   docker push <your-username>/react-docker-hw:latest
   ```

## Links
- **GitHub Repository**: [View on GitHub](https://github.com/Elcanzxc/MyStat-HomeWorks/tree/main/Docker%20HomeWorks/exercise%201)
- **Docker Hub Image**: [View on Docker Hub](https://hub.docker.com/r/elcanzxc/react-docker-hw)

---
*Created for MyStat Homework.*

