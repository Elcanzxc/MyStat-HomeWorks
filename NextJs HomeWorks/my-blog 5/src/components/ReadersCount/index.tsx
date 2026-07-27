"use client";

import useSWR from "swr";

const fetcher = (url: string) => fetch(url).then((r) => r.json());

/**
 * Виджет «Читают сейчас: N человек».
 * Использует useSWR с refreshInterval для автоматического обновления данных каждые 5 секунд.
 * useSWR идеально подходит для «живых» данных, которые часто меняются на клиенте.
 */
export default function ReadersCount({ postId }: { postId: string }) {
  const { data, isLoading } = useSWR<{ count: number }>(
    `/api/posts/${postId}/viewers`,
    fetcher,
    { refreshInterval: 5000 }
  );

  if (isLoading) {
    return (
      <span className="inline-flex items-center gap-2 px-3 py-1 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 rounded-full text-sm animate-pulse">
        👁 Загрузка...
      </span>
    );
  }

  return (
    <span className="inline-flex items-center gap-2 px-3 py-1 bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300 rounded-full text-sm">
      👁 Читают сейчас: {data?.count ?? 0} человек
    </span>
  );
}
