"use server";

import { z } from "zod";
import { revalidatePath } from "next/cache";

/**
 * Zod-схема валидации комментария:
 * - author: строка, минимум 2 символа
 * - text: строка, минимум 5 символов
 */
const commentSchema = z.object({
  author: z.string().min(2, "Имя автора должно содержать минимум 2 символа"),
  text: z.string().min(5, "Текст комментария должен содержать минимум 5 символов"),
});

export type CommentState = {
  errors?: {
    author?: string[];
    text?: string[];
  };
  message?: string;
  success?: boolean;
};

/**
 * Server Action для добавления комментария.
 * Валидация через zod, после успешной отправки — revalidatePath.
 */
export async function addComment(
  prevState: CommentState,
  formData: FormData
): Promise<CommentState> {
  const postId = formData.get("postId") as string;

  const validatedFields = commentSchema.safeParse({
    author: formData.get("author"),
    text: formData.get("text"),
  });

  if (!validatedFields.success) {
    return {
      errors: validatedFields.error.flatten().fieldErrors,
      message: "Ошибка валидации. Проверьте заполненные поля.",
      success: false,
    };
  }

  // Имитация сохранения комментария (в реальном приложении — запись в БД)
  console.log("Новый комментарий:", {
    postId,
    ...validatedFields.data,
  });

  // 3. revalidatePath после успешной отправки комментария —
  // обновляет кеш страницы статьи, чтобы новый комментарий отобразился
  revalidatePath(`/blog/${postId}`);

  return {
    message: "Комментарий успешно добавлен!",
    success: true,
  };
}
