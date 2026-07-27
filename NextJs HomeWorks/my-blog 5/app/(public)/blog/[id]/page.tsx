import Link from "next/link";
import { notFound } from "next/navigation";
import { Suspense } from "react";
import SimilarArticles from "./SimilarArticles";
import TagBadge from "@/components/TagBadge";
import ReadersCount from "@/components/ReadersCount";
import CommentForm from "@/components/CommentForm";

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
    <article className="dark:text-white">
      {/* 1. Виджет «Читают сейчас: N человек» через useSWR */}
      <div className="flex items-center justify-between mb-4 flex-wrap gap-2">
        <h1 className="text-3xl font-bold dark:text-white">{post.title}</h1>
        <ReadersCount postId={id} />
      </div>

      <p className="text-gray-700 dark:text-gray-300 leading-relaxed mb-6">{post.text}</p>

      <div className="my-4">
        <span className="font-bold mr-2 dark:text-gray-300">Теги:</span>
        {post.tags.map((tag) => (
          <TagBadge key={tag} tag={tag} />
        ))}
      </div>

      <h2 className="text-2xl font-semibold mt-8 mb-4 dark:text-white">Похожие Статьи:</h2>
      <Suspense fallback={<p className="text-gray-500 dark:text-gray-400">Загрузка похожих статей...</p>}>
        <SimilarArticles currentTags={post.tags} currentId={post.id} />
      </Suspense>

      {/* 2. Форма комментария на zod + useActionState */}
      <CommentForm postId={id} />
    </article>
  );
}
