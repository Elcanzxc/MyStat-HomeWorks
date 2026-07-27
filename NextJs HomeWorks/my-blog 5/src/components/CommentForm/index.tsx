"use client";

import { useActionState } from "react";
import { addComment, type CommentState } from "@/actions/addComment";

const initialState: CommentState = {
  errors: {},
  message: "",
  success: false,
};

/**
 * Форма комментария с валидацией через zod + useActionState (React 19).
 * - author: минимум 2 символа
 * - text: минимум 5 символов
 * Ошибки валидации отображаются под каждым полем.
 */
export default function CommentForm({ postId }: { postId: string }) {
  const [state, formAction, isPending] = useActionState(addComment, initialState);

  return (
    <div className="mt-8 p-6 bg-gray-50 dark:bg-gray-800 rounded-xl border border-gray-200 dark:border-gray-700">
      <h3 className="text-xl font-semibold mb-4 dark:text-white">
        💬 Оставить комментарий
      </h3>

      <form action={formAction} className="space-y-4">
        <input type="hidden" name="postId" value={postId} />

        {/* Поле автора */}
        <div>
          <label
            htmlFor="author"
            className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1"
          >
            Автор
          </label>
          <input
            id="author"
            name="author"
            type="text"
            placeholder="Ваше имя (мин. 2 символа)"
            className="w-full px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-lg bg-white dark:bg-gray-700 dark:text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all"
          />
          {state.errors?.author && (
            <p className="mt-1 text-sm text-red-500">{state.errors.author[0]}</p>
          )}
        </div>

        {/* Поле комментария */}
        <div>
          <label
            htmlFor="text"
            className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1"
          >
            Комментарий
          </label>
          <textarea
            id="text"
            name="text"
            rows={4}
            placeholder="Ваш комментарий (мин. 5 символов)"
            className="w-full px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-lg bg-white dark:bg-gray-700 dark:text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all resize-vertical"
          />
          {state.errors?.text && (
            <p className="mt-1 text-sm text-red-500">{state.errors.text[0]}</p>
          )}
        </div>

        <button
          type="submit"
          disabled={isPending}
          className="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-all duration-200"
        >
          {isPending ? "Отправка..." : "Отправить комментарий"}
        </button>

        {/* Сообщение об успехе / ошибке */}
        {state.message && !state.errors?.author && !state.errors?.text && (
          <p
            className={`text-sm ${
              state.success ? "text-green-500" : "text-red-500"
            }`}
          >
            {state.message}
          </p>
        )}
      </form>
    </div>
  );
}
