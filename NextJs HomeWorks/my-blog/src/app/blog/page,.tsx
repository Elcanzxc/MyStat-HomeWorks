import Link from "next/link";

const articles = [
  { id: 1, title: "Первая статья", slug: "post-1" },
  { id: 2, title: "Вторая статья", slug: "post-2" },
  { id: 3, title: "Третья статья", slug: "post-3" },
];

export default function Page() {
  return (
    <div>
      {articles.map((article) => (
        <Link href={`/blog/${article.slug}`}>
          <div className="card">
            <h2>{article.title}</h2>
            <p>Читать далее...</p>
          </div>
        </Link>
      ))}
    </div>
  );
}
