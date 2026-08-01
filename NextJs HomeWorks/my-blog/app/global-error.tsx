"use client";
export default function GlobalError({
  error,
  reset,
}: {
  error: Error;
  reset: () => void;
}) {
  return (
    <html>
      <body>
        <p>Global Error {error.message}</p>
      </body>
    </html>
  );
}
