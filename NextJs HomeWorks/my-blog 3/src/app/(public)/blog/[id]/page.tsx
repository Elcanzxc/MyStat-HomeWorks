import Link from "next/link";
import { notFound } from "next/navigation";
import { Suspense } from "react";
import SimilarArticles from "./SimilarArticles";

const blog = [
  {
    id: 1,
    title: "Статья №1",
    text: "Сегодня мы расскажем о новых технологиях, которые помогают сделать повседневные задачи проще и эффективнее.",
    tags: ["nextjs", "react"],
  },

  {
    id: 2,
    title: "Статья №2",
    text: "В этом посте делимся полезными советами по развитию навыков, организации времени и поиску вдохновения.",
    tags: ["javascript", "react"],
  },

  {
    id: 3,
    title: "Статья №3",
    text: "Интересные идеи появляются тогда, когда мы экспериментируем и пробуем что-то новое.",
    tags: ["design", "frontend"],
  },
];

export default async function BlogPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const post = blog.find((item) => item.id === Number(id));

  if (!post) {
    notFound();
  }

  return (
    <article>
      <h1>{post.title}</h1>
      <p>{post.text}</p>

      <p>
        Теги:{" "}
        {post.tags.map((tag) => (
          <Link key={tag} href={`/blog/tags/${tag}`}> #{tag}</Link>
        ))}
      </p>

      <h2>Похожие Статьи:</h2>
      <Suspense fallback={<p>Загрузка похожих статей...</p>}>
        <SimilarArticles currentTags={post.tags} currentId={post.id} />
      </Suspense>
    </article>
  );
}
