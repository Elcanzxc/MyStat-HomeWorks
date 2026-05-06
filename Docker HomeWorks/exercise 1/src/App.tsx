import './index.css';

const FeatureCard = ({ title, description, icon }: { title: string; description: string; icon: string }) => (
  <div className="glass p-8 hover:scale-105 transition-transform duration-300 animate-fade-in">
    <div className="text-4xl mb-4">{icon}</div>
    <h3 className="text-xl font-bold mb-2">{title}</h3>
    <p className="text-slate-400">{description}</p>
  </div>
);

function App() {
  return (
    <div className="gradient-bg min-h-screen">
      <nav className="container py-6 flex justify-between items-center">
        <div className="text-2xl font-black tracking-tighter gradient-text">DOCKERIZED</div>
        <div className="flex gap-8 text-sm font-medium text-slate-300">
          <a href="#" className="hover:text-white transition-colors">Home</a>
          <a href="#" className="hover:text-white transition-colors">About</a>
          <a href="#" className="hover:text-white transition-colors">Container</a>
        </div>
      </nav>

      <main className="container pt-20 pb-32">
        <section className="text-center max-w-3xl mx-auto mb-24">
          <h1 className="text-6xl md:text-7xl font-extrabold mb-6 tracking-tight animate-fade-in">
            React App <span className="gradient-text">In Docker</span>
          </h1>
          <p className="text-xl text-slate-400 mb-10 animate-fade-in" style={{ animationDelay: '0.2s' }}>
            A premium demonstration of containerized frontend architecture. Built with Vite, 
            optimized for production, and served through Nginx.
          </p>
          <div className="flex gap-4 justify-center animate-fade-in" style={{ animationDelay: '0.4s' }}>
            <button className="bg-indigo-600 hover:bg-indigo-500 text-white px-8 py-4 rounded-full font-bold transition-all shadow-lg shadow-indigo-500/20">
              Get Started
            </button>
            <button className="glass px-8 py-4 rounded-full font-bold hover:bg-white/10 transition-all">
              View Source
            </button>
          </div>
        </section>

        <section className="grid md:grid-cols-3 gap-8">
          <FeatureCard 
            icon="🚀" 
            title="Fast Build" 
            description="Vite provides the fastest development environment and optimized production bundles."
          />
          <FeatureCard 
            icon="🐳" 
            title="Dockerized" 
            description="Multi-stage builds ensure small image sizes and secure production environments."
          />
          <FeatureCard 
            icon="🌐" 
            title="Scalable" 
            description="Easily deployable to any cloud provider or container orchestration system."
          />
        </section>
      </main>

      <footer className="container py-12 border-t border-white/10 text-center text-slate-500 text-sm">
        <p>© 2024 Dockerized React App. Built with ❤️ for MyStat Homework.</p>
      </footer>
    </div>
  );
}

export default App;
