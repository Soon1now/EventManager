
export default function ColorPalette() {
  const colors = [
    // Brand
    { name: "primary", className: "bg-primary", text: "text-white" },
    { name: "primary-hover", className: "bg-primary-hover", text: "text-white" },
    { name: "primary-active", className: "bg-primary-active", text: "text-white" },
    { name: "primary-light", className: "bg-primary-light", text: "text-ink-900" },
    { name: "primary-soft", className: "bg-primary-soft", text: "text-ink-900" },


    // Accent
    { name: "accent", className: "bg-accent", text: "text-ink-900" },
    { name: "info", className: "bg-info", text: "text-ink-900" },

    // Neutral
    { name: "surface", className: "bg-surface", text: "text-ink-900" },
    { name: "background", className: "bg-background", text: "text-ink-900" },
    { name: "surface-secondary", className: "bg-surface-secondary", text: "text-ink-900" },
    { name: "border", className: "bg-border", text: "text-ink-900" },

    { name: "text", className: "bg-text", text: "text-white" },
    { name: "text-secondary", className: "bg-text-secondary", text: "text-white" },
    { name: "text-muted", className: "bg-text-muted", text: "text-ink-900" },

    // Status
    { name: "success", className: "bg-success", text: "text-white" },
    { name: "warning", className: "bg-warning", text: "text-white" },
    { name: "warning-soft", className: "bg-warning-soft", text: "text-ink-900" },
    { name: "error", className: "bg-error", text: "text-white" },
    { name: "error-soft", className: "bg-error-soft", text: "text-ink-900" },

    // Other
    { name: "disabled", className: "bg-disabled", text: "text-ink-900" },
    { name: "focus", className: "bg-focus", text: "text-ink-900" },
    { name: "overlay", className: "bg-overlay", text: "text-white" },
  ];

  return (
    <div className="min-h-screen bg-background p-6">
      <div className="mx-auto max-w-6xl">
        <h1 className="mb-6 text-2xl font-bold text-text">
          Цветовая палитра
        </h1>

        <div className="grid grid-cols-2 gap-4 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6">
          {colors.map((color) => (
            <div
              key={color.name}
              className={`flex h-28 items-end rounded-card border border-border p-3 ${color.className}`}
            >
              <span
                className={`rounded-md bg-white/20 px-2 py-1 text-xs font-bold ${color.text}`}
              >
                {color.name}
              </span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
