"use server"
import z from "zod";
import { commentSchema } from "./schema";
import { revalidatePath } from "next/cache";
import { comments } from "./comments-store";


export type FormState = {
  errors?: {
    author?: string[];
    text?: string[];
  };
  success?: boolean;
};


export async function submitComment(
  prevState: FormState,
  formData: FormData,
): Promise<FormState> {
  const validateFields = commentSchema.safeParse({
    author: formData.get("author"),
    text: formData.get("text"),
  });

  if (!validateFields.success) {
    const flattened = z.flattenError(validateFields.error);

    return {
      errors: flattened.fieldErrors,
    };
  }

  comments.push(validateFields.data);

  revalidatePath("/blog/");

  return {
    success: true,
  };
}
