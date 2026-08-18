import Comments from "@/src/components/Comments";
import LikeArticleButton from "@/src/components/LikeArticleButton";
import ShowComments from "@/src/components/ShowComments";
import TagBadge from "@/src/components/TagBadge/TagBadge";
import ToggleButton from "@/src/components/ToggleButton";
import TagsCloud from "@/src/components/TagsCloud";
import { articles } from "@/src/utils";
import Link from "next/link";
import BlogArticles from "@/src/components/BlogArticles";



export const revalidate = 30;

export default function Home() {

  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between border-b border-slate-200 dark:border-slate-800 pb-4">
        <h2 className="text-2xl font-bold tracking-tight text-slate-800 dark:text-slate-100">
          Последние записи
        </h2>
        <p className="text-sm text-slate-500 dark:text-slate-400 bg-slate-100 dark:bg-slate-800 px-3 py-1 rounded-full">
          Обновлено:{" "}
          {new Date().toLocaleTimeString("ru-RU", {
            hour: "2-digit",
            minute: "2-digit",
          })}
        </p>
      </div>

      <TagsCloud />

      <BlogArticles>
        <LikeArticleButton />
      </BlogArticles>

   


    </div>
  );
}

// Выбрал ISR , ведь блоги могут увеличиватся( в продакшене), SSG не подходит
// Через каждый запрос чекать блоги нет смысла , SSR не подходит

// Список статей fetch а не use swr потому что это статичные вещи, просто статья которую получил чтобы читать
// если бы нам нужно было эти статьи обновлять на клиенте то уже был бы useSwr
