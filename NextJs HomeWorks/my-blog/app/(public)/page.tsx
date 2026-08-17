
import Comments from "@/src/components/Comments";
import LikeArticleButton from "@/src/components/LikeArticleButton";
import ShowComments from "@/src/components/ShowComments";
import TagBadge from "@/src/components/TagBadge/TagBadge";
import ToggleButton from "@/src/components/ToggleButton";
import TagsCloud from "@/src/components/TagsCloud";
import { articles } from "@/src/utils";
import Link from "next/link";

export const revalidate = 30;

export default function Home() {
  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between border-b border-slate-200 dark:border-slate-800 pb-4">
        <h2 className="text-2xl font-bold tracking-tight text-slate-800 dark:text-slate-100">Последние записи</h2>
        <p className="text-sm text-slate-500 dark:text-slate-400 bg-slate-100 dark:bg-slate-800 px-3 py-1 rounded-full">
          Обновлено: {new Date().toLocaleTimeString('ru-RU', { hour: '2-digit', minute: '2-digit' })}
        </p>
      </div>

      <TagsCloud />

      <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        {articles.map((item) => (
          <div 
            key={item.id} 
            className="group relative flex flex-col justify-between bg-white dark:bg-slate-800 rounded-2xl p-6 shadow-sm border border-slate-100 dark:border-slate-700 hover:shadow-md hover:border-blue-100 dark:hover:border-slate-600 transition-all duration-200"
          >
            <div>
              <div className="flex items-center justify-between mb-3">
                <span className="text-xs font-semibold text-slate-500 dark:text-slate-400">
                  #{item.id}
                </span>
                <div className="flex gap-2">
                  {item.tags.map((tag) => (
                    <TagBadge key={tag} text={tag}/>
                  ))}
                </div>
              </div>
              <h3 className="text-lg font-bold text-slate-900 dark:text-white mb-2 line-clamp-2 group-hover:text-blue-600 dark:group-hover:text-blue-400 transition-colors">
                <Link href={`blog/${item.id}`} className="focus:outline-none">
                  <span className="absolute inset-0" aria-hidden="true" />
                  {item.title}
                </Link>
              </h3>
              <p className="text-slate-600 dark:text-slate-300 text-sm line-clamp-3 mb-4">
                {item.description}
              </p>
            </div>
            
            <div className="relative z-10 pt-4 border-t border-slate-100 dark:border-slate-700 mt-auto">
              <LikeArticleButton />
            </div>
          </div>
        ))}
      </div>
 
    </div>
  );
}

// Выбрал ISR , ведь блоги могут увеличиватся( в продакшене), SSG не подходит
// Через каждый запрос чекать блоги нет смысла , SSR не подходит



// Список статей fetch а не use swr потому что это статичные вещи, просто статья которую получил чтобы читать
// если бы нам нужно было эти статьи обновлять на клиенте то уже был бы useSwr
