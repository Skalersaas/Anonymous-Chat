/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./*.html",      // Match all HTML files in the root directory
    "./src/**/*.{js,jsx,ts,tsx,vue}", // Match source files for dynamic frameworks like Vue or React
  ],
  theme: {
    extend: {
      colors: {
        primary: "var(--color-primary)",
        secondary: "var(--color-secondary)",
        background: "var(--color-background)",
        companion: "var(--color-companion)",
        attachments: "var(--color-attachments)",
        sendBg: "var(--color-send-bg)",
        send: "var(--color-send)",
        textPrimary: "var(--color-text-primary)",
        textInput: "var(--color-text-input)",
        inputEnabled: "var(--color-input-enabled)",
        inputBorder: "var(--color-input-border)",
        msgBgIn: "var(--color-msg-bg-in)",
        msgBgOut: "var(--color-msg-bg-out)",
        msgBgSys: "var(--color-msg-bg-sys)",
        msgBorderSys: "var(--color-msg-border-sys)",

        msgTextIn: "var(--color-msg-text-in)",
        msgTextOut: "var(--color-msg-text-out)",
        msgTextSys: "var(--color-msg-text-sys)",


        actionProfile: "var(--color-action-profile)",


        profileBg: "var(--color-profile-bg)",
        profileSubmit: "var(--color-profile-submit)"
      },
    },
  },
  plugins: [],
}
