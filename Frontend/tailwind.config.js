/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./*.html",      // Match all HTML files in the root directory
    "./src/**/*.{js,jsx,ts,tsx,vue}", // Match source files for dynamic frameworks like Vue or React
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
