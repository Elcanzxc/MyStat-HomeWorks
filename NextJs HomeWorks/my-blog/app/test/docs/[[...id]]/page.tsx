import { delay } from "@/src/utils";
import { notFound } from "next/navigation";

export default async function page({ params,}: {params: Promise<{ id: string[] }>}) {
  const { id } = await params;

  if (id.includes("123")) {
    notFound();
  }
 
  return (
    <div>
      <h3>
        Dynamic Page:{" "}
        {id.map((item) => (
          <p key={item}>{item}</p>
        ))}
      </h3>
    </div>
  );
}
