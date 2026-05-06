import './index.css';

interface Painting {
  title: string;
  artist: string;
  year: string;
  url: string;
  description: string;
}

const paintings: Painting[] = [
  {
    title: "The Starry Night",
    artist: "Vincent van Gogh",
    year: "1889",
    url: "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Van_Gogh_-_Starry_Night_-_Google_Art_Project.jpg/1280px-Van_Gogh_-_Starry_Night_-_Google_Art_Project.jpg",
    description: "A dreamlike interpretation of the artist's asylum room view, featuring a swirling night sky."
  },
  {
    title: "Mona Lisa",
    artist: "Leonardo da Vinci",
    year: "1503",
    url: "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Mona_Lisa%2C_by_Leonardo_da_Vinci%2C_from_C2RMF_retouched.jpg/800px-Mona_Lisa%2C_by_Leonardo_da_Vinci%2C_from_C2RMF_retouched.jpg",
    description: "The world's most famous portrait, known for the subject's enigmatic expression."
  },
  {
    title: "The Great Wave",
    artist: "Katsushika Hokusai",
    year: "1831",
    url: "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Great_Wave_off_Kanagawa2.jpg/1280px-Great_Wave_off_Kanagawa2.jpg",
    description: "A masterpiece of Japanese ukiyo-e art, depicting a giant wave threatening boats."
  },
  {
    title: "Girl with a Pearl Earring",
    artist: "Johannes Vermeer",
    year: "1665",
    url: "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Johannes_Vermeer_%281632-1675%29_-_The_Girl_With_The_Pearl_Earring_%281665%29.jpg/800px-Johannes_Vermeer_%281632-1675%29_-_The_Girl_With_The_Pearl_Earring_%281665%29.jpg",
    description: "Often called the 'Mona Lisa of the North', this painting focuses on light and texture."
  },
  {
    title: "The Night Watch",
    artist: "Rembrandt",
    year: "1642",
    url: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/The_Night_Watch_-_HD.jpg/1280px-The_Night_Watch_-_HD.jpg",
    description: "A monumental group portrait famous for its colossal size and dramatic use of light."
  },
  {
    title: "The Kiss",
    artist: "Gustav Klimt",
    year: "1907",
    url: "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/The_Kiss_-_Gustav_Klimt_-_Google_Art_Project.jpg/800px-The_Kiss_-_Gustav_Klimt_-_Google_Art_Project.jpg",
    description: "A symbolist masterpiece from Klimt's 'Golden Phase', depicting a couple entwined in love."
  }
];

const PaintingCard = ({ painting }: { painting: Painting }) => (
  <div className="painting-card">
    <div className="painting-frame">
      <div className="painting-img-wrapper">
        <img src={painting.url} alt={painting.title} className="painting-img" />
        <div className="spotlight"></div>
      </div>
    </div>
    <div className="painting-info">
      <div className="painting-artist">{painting.artist}, {painting.year}</div>
      <h3 className="painting-title">{painting.title}</h3>
      <p className="painting-desc">{painting.description}</p>
    </div>
  </div>
);

function App() {
  return (
    <div className="museum-container">
      <header className="hero-museum">
        <div style={{ maxWidth: '800px' }}>
          <h1 style={{ fontSize: '5rem', marginBottom: '1rem', fontWeight: 900 }}>GRAND GALLERY</h1>
          <p style={{ fontSize: '1.2rem', letterSpacing: '4px', textTransform: 'uppercase', color: 'var(--gold)' }}>
            Virtual Exhibition of Eternal Masterpieces
          </p>
          <button className="gold-btn">Explore Collection</button>
        </div>
      </header>

      <main style={{ maxWidth: '1400px', margin: '0 auto', padding: '100px 2rem' }}>
        <div style={{ textAlign: 'center', marginBottom: '80px' }}>
          <h2 style={{ fontSize: '3rem', marginBottom: '20px' }}>Most Celebrated Works</h2>
          <div style={{ width: '60px', height: '2px', background: 'var(--gold)', margin: '0 auto' }}></div>
        </div>

        <div className="museum-grid">
          {paintings.map((p, i) => (
            <PaintingCard key={i} painting={p} />
          ))}
        </div>
      </main>

      <footer style={{ padding: '80px 2rem', borderTop: '1px solid #222', textAlign: 'center', color: '#555' }}>
        <p style={{ letterSpacing: '2px', textTransform: 'uppercase', fontSize: '0.8rem' }}>
          © 2024 Digital Art Preservation • Virtual Museum Project
        </p>
      </footer>
    </div>
  );
}

export default App;
