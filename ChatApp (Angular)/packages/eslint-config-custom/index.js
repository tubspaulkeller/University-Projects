/**
 * Shareable minimal ESLint config supporting both TypeScript (for source code,
 * tests, and scripts) and plain JavaScript (mostly for configuration files).
 */
module.exports = {
  overrides: [
    {
      files: ['*.ts'],
      parser: '@typescript-eslint/parser',
      extends: ['eslint:recommended', 'plugin:@typescript-eslint/recommended', 'turbo', 'prettier'],
      plugins: ['@typescript-eslint'],
    },
    {
      files: ['*.js'],
      parser: '@babel/eslint-parser',
      parserOptions: {
        requireConfigFile: false,
      },
      env: {
        browser: true,
        jest: true,
        node: true,
      },
    },
  ],
};
