# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

The project uses webpack with multiple build configurations:

- `npm run build:vendor:dev` - Build vendor dependencies (development)
- `npm run build:vendor:pro` - Build vendor dependencies (production)
- `npm run build:app:dev` - Build application with watch mode (development)
- `npm run build:app:pro` - Build application (production)
- `npm run build:all:dev` - Build both vendor and app (development)
- `npm run build:all:pro` - Build both vendor and app (production)
- `npm run build:page:dev` - Build page-specific bundles with watch mode
- `npm start` - Start webpack dev server with hot reloading

## Test Commands

- `npm test` - Run Jest tests
- `npm run test:watch` - Run tests in watch mode
- `npm run test:coverage` - Generate test coverage report

## Architecture

This is a React-based admin portal with the following structure:

### Frontend Architecture
- **Framework**: React 16.14 with Redux for state management
- **Build Tool**: Webpack 4 with multiple configurations
- **Testing**: Jest with Enzyme for component testing
- **Styling**: SASS/SCSS with Bootstrap 4

### Directory Structure
- `Areas/Portal/www/source/js/` - JavaScript source code
  - `actions/` - Redux actions
  - `components/` - Reusable React components
  - `layouts/` - Layout components
  - `modules/` - Feature modules
  - `pages/` - Page-specific components organized by feature (administrator, affiliate-info, customer, domain, etc.)
  - `pagelayout/` - Page layout templates
  - `reducers/` - Redux reducers
- `Areas/Portal/www/source/style/` - SASS/SCSS styles
  - `abstracts/` - Variables, mixins, functions
  - `base/` - Base styles
  - `components/` - Component-specific styles
  - `layout/` - Layout styles
  - `pages/` - Page-specific styles
  - `themes/` - Theme configurations

### Key Features
The admin portal includes modules for:
- Administrator management
- Affiliate information
- Customer management
- Domain management
- Email history
- Landing page management
- Media gallery
- Member management
- Operations
- Payment requests
- Reports
- Marketing accounts

### Webpack Configuration
- Multiple entry points for different page bundles
- Separate vendor and application bundles
- Development server with hot reloading
- Production optimizations with CSS extraction and minification

### Testing Setup
- Jest configured with Enzyme adapter for React 16
- Test files located in `__test__` directories within components
- Coverage excludes `pagelayout` and `thirdparty-product` directories