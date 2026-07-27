import LikeArticleButton from "@/components/LikeArticleButton";
import Link from "next/link";

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