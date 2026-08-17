import z from "zod";

export const commentSchema = z.object({
    author: z.string().min(2, 'Имя должо быть не короче 2 символов'),
    text: z.string().min(5, "Комментарий должен быть не короче 5 символов")
})

