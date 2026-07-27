import LikeArticleButton from "@/components/LikeArticleButton";
import Link from "next/link";

/**
 * 4. Почему список статей — fetch (серверный компонент), а не useSWR?
 *
 * Этот компонент — серверный (Server Component), данные доступны на этапе рендеринга на сервере.
 * Это эквивалент fetch/getServerSideProps из прежних версий Next.js.
 *
 * Причины использования fetch (серверного рендеринга) вместо useSWR:
 *
 * 1. SEO: HTML формируется на сервере — поисковые роботы (Google, Yandex)
 *    видят готовый контент без необходимости выполнять JavaScript.
 *    useSWR — клиентский хук, данные загружаются в браузере после гидрации,
 *    поэтому поисковики не увидят содержимое статей.
 *
 * 2. Производительность: Контент готов при первом рендере — пользователь видит
 *    список статей мгновенно, без loading-спиннера.
 *    useSWR сначала покажет пустую страницу или «загрузка...», пока данные не придут.
 *
 * 3. Кеширование: Next.js умеет кешировать результаты fetch на сервере
 *    (static generation / ISR), снижая нагрузку на API.
 *    useSWR кеширует только на стороне клиента (в памяти браузера).
 *
 * 4. useSWR лучше подходит для данных, которые часто меняются и не критичны для SEO —
 *    например, виджет «Читают сейчас: N человек», уведомления, чат.
 */
export default function Home() {
  return (
    <main className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6 dark:text-white">
        Home Task
      </h1>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div className="p-6 border rounded-lg shadow-md hover:shadow-lg transition-shadow bg-white dark:bg-gray-800 dark:border-gray-700 dark:text-white flex flex-col justify-between">
          <Link 
            className="text-xl text-blue-600 hover:underline dark:text-blue-400 mb-4 inline-block"
            href="/blog/1"
          >
            Статья №1
          </Link>
          <LikeArticleButton />
        </div>

        <div className="p-6 border rounded-lg shadow-md hover:shadow-lg transition-shadow bg-white dark:bg-gray-800 dark:border-gray-700 dark:text-white flex flex-col justify-between">
          <Link
            className="text-xl text-blue-600 hover:underline dark:text-blue-400 mb-4 inline-block"
            href="/blog/2"
          >
            Статья №2
          </Link>
          <LikeArticleButton />
        </div>

        <div className="p-6 border rounded-lg shadow-md hover:shadow-lg transition-shadow bg-white dark:bg-gray-800 dark:border-gray-700 dark:text-white flex flex-col justify-between">
          <Link
            className="text-xl text-blue-600 hover:underline dark:text-blue-400 mb-4 inline-block"
            href="/blog/3"
          >
            Статья №3
          </Link>
          <LikeArticleButton />
        </div>
      </div>
    </main>
  );
}