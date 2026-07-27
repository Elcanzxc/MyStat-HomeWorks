import Link from "next/link";
import { notFound } from "next/navigation";
import { Suspense } from "react";
import SimilarArticles from "./SimilarArticles";

import { blog } from "@/data/blog";

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
