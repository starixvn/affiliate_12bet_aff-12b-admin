# Landing Page Asset Uploader

A comprehensive media management system for landing page builders with drag & drop uploads, advanced filtering, and seamless page builder integration.

## 🚀 Features

### Core Asset Management
- **Drag & Drop Upload**: Intuitive file upload with visual feedback
- **Module-Specific Validation**: Different rules for Image, Contact, Form, and Link modules
- **Progress Tracking**: Real-time upload progress with cancellation support
- **Advanced Filtering**: Search by name, file type, size, and more
- **Bulk Operations**: Select and manage multiple files simultaneously
- **Grid & Table Views**: Flexible display options for different workflows

### Page Builder Integration
- **Media Picker Modal**: Select existing media for modules
- **Media Assignment**: Complete media management per module
- **Real-time Preview**: Instant visual feedback in page builder
- **Module Constraints**: Respect maximum assignments and file type limits

### Technical Features
- **Client & Server Validation**: Comprehensive file validation
- **Image Optimization**: Automatic resize and compression
- **Thumbnail Generation**: Fast preview generation
- **Constants-Based**: No magic numbers, maintainable configuration
- **Redux State Management**: Predictable state updates
- **Responsive Design**: Works on all screen sizes

## 📁 Project Structure

```
asset-uploader/
├── actions/
│   ├── types.js                 # Redux action constants
│   └── assetActions.js          # Complete actions with async thunks
├── reducers/
│   └── assetReducer.js          # Redux state management
├── components/
│   ├── AssetUploaderContainer.jsx # Main container component
│   ├── ModuleSelector.jsx       # Module selection dropdown
│   ├── FileDropZone.jsx         # Drag & drop upload area
│   ├── MediaGallery.jsx         # Grid/table media display
│   ├── FileFilters.jsx          # Search and filtering
│   ├── BulkActions.jsx          # Bulk operations toolbar
│   ├── UploadProgress.jsx       # Progress tracking display
│   ├── MediaStats.jsx           # Statistics and analytics
│   ├── MediaModal.jsx           # Media preview and editing
│   ├── MediaPicker.jsx          # Page builder media selection
│   ├── MediaAssignment.jsx      # Module media management
│   └── index.js                 # Component exports
├── libs/
│   ├── uploadService.js         # Core upload functionality
│   └── pageBuilderService.js    # Page builder API integration
├── examples/
│   └── PageBuilderIntegration.jsx # Complete integration example
└── index.jsx                    # Application entry point
```

## 🛠️ Backend Architecture

### Controllers
- **`AssetUploaderController.cs`**: Core media upload and management
- **`PageBuilderController.cs`**: Page builder integration endpoints

### Services
- **`ModuleMediaGalleryService.cs`**: Business logic for media operations
- **`LandingPageModuleService.cs`**: Module management
- **`SystemFile.cs`**: File system operations

### Key Features
- **BannerFolders Integration**: Multi-server file storage support
- **Image Optimization**: Using BannerHelper for validation and processing
- **Constants-Based**: LandingPageConstants for maintainable configuration
- **Module-Specific Logic**: Different validation rules per module type

## 🚀 Quick Start

### 1. Basic Asset Uploader

```jsx
import React from 'react';
import { Provider } from 'react-redux';
import { AssetUploaderContainer } from './components';
import assetReducer from './reducers/assetReducer';
import { configureStore } from '../../../modules/store';

const AssetUploaderApp = () => {
    const store = configureStore({ assetUploader: assetReducer }, {});
    
    return (
        <Provider store={store}>
            <AssetUploaderContainer />
        </Provider>
    );
};

export default AssetUploaderApp;
```

### 2. Page Builder Integration

```jsx
import React from 'react';
import { MediaPicker, MediaAssignment } from './components';

const PageBuilderModule = ({ moduleId, pageId }) => {
    return (
        <MediaAssignment
            moduleId={moduleId}
            moduleType={1} // Image module
            pageId={pageId}
            moduleName="Hero Background"
            allowMultiple={false}
            maxAssignments={1}
            onMediaAssigned={(media) => console.log('Media assigned:', media)}
            onMediaRemoved={(mediaId) => console.log('Media removed:', mediaId)}
        />
    );
};
```

### 3. Media Picker Modal

```jsx
import React, { useState } from 'react';
import { MediaPicker } from './components';

const MediaSelector = ({ moduleId }) => {
    const [showPicker, setShowPicker] = useState(false);
    
    const handleMediaSelect = (selectedMedia) => {
        console.log('Selected media:', selectedMedia);
        setShowPicker(false);
    };
    
    return (
        <>
            <button onClick={() => setShowPicker(true)}>
                Select Media
            </button>
            
            <MediaPicker
                show={showPicker}
                onHide={() => setShowPicker(false)}
                onSelectMedia={handleMediaSelect}
                moduleId={moduleId}
                moduleType={1}
                allowMultiple={true}
                title="Select Images"
            />
        </>
    );
};
```

## 🎨 Component API

### AssetUploaderContainer
Main container component with full functionality.

**Props:** None (uses Redux for state management)

### MediaPicker
Modal for selecting media files.

```jsx
<MediaPicker
    show={boolean}                    // Show/hide modal
    onHide={function}                 // Close handler
    onSelectMedia={function}          // Selection handler
    moduleId={number}                 // Module ID for filtering
    moduleType={number}               // Module type for validation
    allowMultiple={boolean}           // Allow multiple selection
    selectedMediaIds={array}          // Pre-selected media IDs
    title={string}                    // Modal title
/>
```

### MediaAssignment
Complete media management for a module.

```jsx
<MediaAssignment
    moduleId={number}                 // Module ID
    moduleType={number}               // Module type
    pageId={number}                   // Page ID
    moduleName={string}               // Display name
    onMediaAssigned={function}        // Assignment handler
    onMediaRemoved={function}         // Removal handler
    allowMultiple={boolean}           // Allow multiple assignments
    maxAssignments={number}           // Maximum assignments
/>
```

### FileDropZone
Drag & drop upload area.

```jsx
<FileDropZone
    onFilesSelected={function}        // File selection handler
    selectedModule={object}           // Selected module info
    isUploading={boolean}             // Upload state
    dragOver={boolean}                // Drag state
/>
```

## 🔧 Configuration

### Module Types
```javascript
const ModuleTypes = {
    Image: 1,      // Image modules - strict image validation
    Contact: 2,    // Contact modules - images and documents
    Form: 3,       // Form modules - images and documents
    Link: 4        // Link modules - no file uploads
};
```

### File Constraints
```javascript
const FileConstraints = {
    MaxFileSize: 5 * 1024 * 1024,    // 5MB
    MaxImageWidth: 2000,              // 2000px
    MaxImageHeight: 2000,             // 2000px
    AllowedImageTypes: ['.jpg', '.jpeg', '.png', '.gif'],
    ThumbnailSize: 150                // 150px
};
```

### Module-Specific Limits
```javascript
const ModuleImageLimits = {
    Background: { width: 1920, height: 1080 },
    Hero: { width: 1200, height: 600 },
    Logo: { width: 400, height: 200 },
    Button: { width: 300, height: 100 }
};
```

## 🔌 API Endpoints

### Asset Uploader
- `POST /LandingPage/AssetUploader/UploadModuleMedia` - Upload files
- `POST /LandingPage/AssetUploader/GetModuleMediaList` - Get media list
- `POST /LandingPage/AssetUploader/DeleteModuleMedia` - Delete media
- `POST /LandingPage/AssetUploader/ValidateMediaFile` - Validate file
- `POST /LandingPage/AssetUploader/GetMediaRootUrl` - Get media root URL
- `POST /LandingPage/AssetUploader/GetModuleMediaStats` - Get statistics

### Page Builder Integration
- `POST /LandingPage/PageBuilder/GetMediaPickerList` - Get picker media list
- `POST /LandingPage/PageBuilder/AssignMediaToModule` - Assign media
- `POST /LandingPage/PageBuilder/RemoveMediaFromModule` - Remove assignment
- `POST /LandingPage/PageBuilder/GetModuleAssignedMedia` - Get assignments

## 🎯 Best Practices

### 1. Error Handling
Always handle errors gracefully with user-friendly messages:

```jsx
try {
    const result = await uploadService.uploadFiles(files, params);
    if (!result.IsSuccessful) {
        setError(result.Message || 'Upload failed');
    }
} catch (err) {
    setError('Network error: ' + err.message);
}
```

### 2. Progress Tracking
Use the built-in progress tracking for better UX:

```jsx
const handleUpload = async (files) => {
    const uploadId = await uploadService.uploadFiles(files, params, (progress) => {
        setUploadProgress(progress);
    });
};
```

### 3. Validation
Always validate files client-side before upload:

```jsx
const validation = uploadService.validateFileClient(file, moduleType);
if (!validation.isValid) {
    setErrors(validation.errors);
    return;
}
```

### 4. Memory Management
Clean up resources and cancel uploads when components unmount:

```jsx
useEffect(() => {
    return () => {
        // Cancel any ongoing uploads
        uploadService.cancelAllUploads();
    };
}, []);
```

## 🔍 Troubleshooting

### Common Issues

**1. Files not uploading**
- Check file size limits (5MB default)
- Verify file types are allowed for module
- Check network connectivity and server capacity

**2. Images not displaying**
- Verify BannerRootUrl configuration
- Check file permissions on server
- Ensure thumbnail generation is working

**3. Validation errors**
- Check LandingPageConstants configuration
- Verify module type mappings
- Review BannerHelper validation rules

**4. Performance issues**
- Enable image optimization
- Use pagination for large media lists
- Implement lazy loading for thumbnails

### Debug Mode
Enable debug logging:

```javascript
uploadService.setDebugMode(true);
```

## 📈 Performance Optimization

### 1. Image Optimization
- Automatic resize for large images
- Compression based on module requirements
- Thumbnail generation for fast loading

### 2. Lazy Loading
- Load media lists on demand
- Progressive image loading
- Virtual scrolling for large lists

### 3. Caching
- Browser caching for media files
- Redux state persistence
- API response caching

## 🔒 Security Considerations

### 1. File Validation
- Server-side validation for all uploads
- MIME type checking
- File size limits enforced
- Malicious file detection

### 2. Access Control
- Function-based authorization
- User permission checks
- Anti-forgery token validation

### 3. File Storage
- Secure file storage paths
- No direct file access
- Proper file permissions

## 🤝 Contributing

### Development Setup
1. Clone the repository
2. Install dependencies: `npm install`
3. Start development server: `npm start`
4. Run tests: `npm test`

### Code Standards
- Follow existing patterns and conventions
- Add comprehensive JSDoc comments
- Include unit tests for new features
- Follow responsive design principles

### Pull Request Process
1. Create feature branch from main
2. Implement changes with tests
3. Update documentation
4. Submit pull request with detailed description

## 📄 License

This project is part of the Fanex Affiliate Admin system and is proprietary software.

## 📞 Support

For technical support or questions:
- Create an issue in the project repository
- Contact the development team
- Check the troubleshooting guide above

---

**Version:** 1.0.0  
**Last Updated:** December 2024  
**Compatibility:** ASP.NET MVC 5, React 16+, C# 7.3
