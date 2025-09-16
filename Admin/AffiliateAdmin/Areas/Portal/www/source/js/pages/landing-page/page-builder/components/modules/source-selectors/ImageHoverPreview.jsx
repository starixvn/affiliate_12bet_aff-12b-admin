import React, { useState, useCallback, useRef, useEffect } from 'react';

const ImageHoverPreview = ({ 
    src, 
    alt, 
    className = "", 
    style = {}, 
    maxPreviewSize = { width: 400, height: 300 },
    children 
}) => {
    const [showPreview, setShowPreview] = useState(false);
    const [previewPosition, setPreviewPosition] = useState({ x: 0, y: 0 });
    const [imageDimensions, setImageDimensions] = useState({ width: 300, height: 200 });
    const [imageLoaded, setImageLoaded] = useState(false);
    const triggerRef = useRef(null);
    const previewRef = useRef(null);
    const imageRef = useRef(null);

    const calculateOptimalSize = useCallback((naturalWidth, naturalHeight) => {
        const maxWidth = maxPreviewSize.width;
        const maxHeight = maxPreviewSize.height;
        
        // If image is smaller than max size, use natural dimensions
        if (naturalWidth <= maxWidth && naturalHeight <= maxHeight) {
            return { width: naturalWidth, height: naturalHeight };
        }
        
        // Calculate aspect ratio
        const aspectRatio = naturalWidth / naturalHeight;
        
        // Determine which dimension to constrain by
        let width, height;
        
        if (naturalWidth / maxWidth > naturalHeight / maxHeight) {
            // Width is the constraining factor
            width = maxWidth;
            height = maxWidth / aspectRatio;
        } else {
            // Height is the constraining factor
            height = maxHeight;
            width = maxHeight * aspectRatio;
        }
        
        return { width: Math.round(width), height: Math.round(height) };
    }, [maxPreviewSize]);

    // Load image to get natural dimensions
    useEffect(() => {
        if (!src) return;
        
        const img = new Image();
        img.onload = () => {
            const optimalSize = calculateOptimalSize(img.naturalWidth, img.naturalHeight);
            setImageDimensions(optimalSize);
            setImageLoaded(true);
        };
        img.onerror = () => {
            setImageLoaded(false);
        };
        img.src = src;
    }, [src, calculateOptimalSize]);

    const handleMouseEnter = useCallback((e) => {
        if (!src || !imageLoaded) return;
        
        const rect = e.currentTarget.getBoundingClientRect();
        const viewportWidth = window.innerWidth;
        const viewportHeight = window.innerHeight;
        const previewWidth = imageDimensions.width + 16; // Add padding
        const previewHeight = imageDimensions.height + 16; // Add padding
        
        let x = rect.right + 10; // Default: to the right of the trigger
        let y = rect.top;
        
        // Check if preview would go off the right edge
        if (x + previewWidth > viewportWidth - 20) {
            x = rect.left - previewWidth - 10; // Show to the left
        }
        
        // Check if preview would go off the bottom edge
        if (y + previewHeight > viewportHeight - 20) {
            y = viewportHeight - previewHeight - 20; // Adjust to fit
        }
        
        // Check if preview would go off the top edge
        if (y < 20) {
            y = 20;
        }
        
        setPreviewPosition({ x, y });
        setShowPreview(true);
    }, [src, imageLoaded, imageDimensions]);

    const handleMouseLeave = useCallback(() => {
        setShowPreview(false);
    }, []);

    const handleMouseMove = useCallback((e) => {
        if (!showPreview || !src || !imageLoaded) return;
        
        const rect = e.currentTarget.getBoundingClientRect();
        const viewportWidth = window.innerWidth;
        const viewportHeight = window.innerHeight;
        const previewWidth = imageDimensions.width + 16; // Add padding
        const previewHeight = imageDimensions.height + 16; // Add padding
        
        let x = rect.right + 10;
        let y = e.clientY - (previewHeight / 2); // Center vertically on cursor
        
        // Check boundaries
        if (x + previewWidth > viewportWidth - 20) {
            x = rect.left - previewWidth - 10;
        }
        
        if (y + previewHeight > viewportHeight - 20) {
            y = viewportHeight - previewHeight - 20;
        }
        
        if (y < 20) {
            y = 20;
        }
        
        setPreviewPosition({ x, y });
    }, [showPreview, src, imageLoaded, imageDimensions]);

    // Close preview when clicking outside
    useEffect(() => {
        const handleClickOutside = (e) => {
            if (showPreview && 
                triggerRef.current && 
                !triggerRef.current.contains(e.target) &&
                previewRef.current &&
                !previewRef.current.contains(e.target)) {
                setShowPreview(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, [showPreview]);

    return (
        <>
            <div
                ref={triggerRef}
                className={`image-hover-trigger ${className}`}
                style={{ 
                    ...style,
                    position: 'relative',
                    cursor: src ? 'pointer' : 'default',
                    transition: 'background-color 0.2s ease',
                    ...(showPreview && src ? { backgroundColor: 'rgba(0, 123, 255, 0.1)' } : {})
                }}
                onMouseEnter={handleMouseEnter}
                onMouseLeave={handleMouseLeave}
                onMouseMove={handleMouseMove}
            >
                {children}
            </div>
            
            {showPreview && src && imageLoaded && (
                <>
                    <div 
                        className="image-preview-backdrop"
                        style={{
                            position: 'fixed',
                            top: 0,
                            left: 0,
                            right: 0,
                            bottom: 0,
                            zIndex: 9998,
                            pointerEvents: 'none'
                        }}
                    />
                    <div
                        ref={previewRef}
                        className="image-hover-preview"
                        style={{
                            position: 'fixed',
                            left: `${previewPosition.x}px`,
                            top: `${previewPosition.y}px`,
                            zIndex: 9999,
                            backgroundColor: 'white',
                            border: '2px solid #dee2e6',
                            borderRadius: '8px',
                            boxShadow: '0 4px 12px rgba(0, 0, 0, 0.15)',
                            padding: '8px',
                            width: `${imageDimensions.width + 16}px`,
                            height: `${imageDimensions.height + 16}px`,
                            pointerEvents: 'none'
                        }}
                    >
                        <img
                            ref={imageRef}
                            src={src}
                            alt={alt}
                            style={{
                                width: `${imageDimensions.width}px`,
                                height: `${imageDimensions.height}px`,
                                objectFit: 'contain',
                                borderRadius: '4px',
                                display: 'block'
                            }}
                            onError={(e) => {
                                e.target.style.display = 'none';
                            }}
                        />
                        
                        {/* Image dimensions display */}
                        <div 
                            style={{
                                position: 'absolute',
                                top: '4px',
                                right: '4px',
                                backgroundColor: 'rgba(0, 0, 0, 0.8)',
                                color: 'white',
                                padding: '2px 6px',
                                fontSize: '10px',
                                borderRadius: '3px',
                                fontFamily: 'monospace',
                                lineHeight: '1.2'
                            }}
                        >
                            {imageRef.current?.naturalWidth || 0} × {imageRef.current?.naturalHeight || 0}
                        </div>
                        
                        {/* Image name caption */}
                        {alt && (
                            <div 
                                style={{
                                    position: 'absolute',
                                    bottom: '4px',
                                    left: '4px',
                                    right: '4px',
                                    backgroundColor: 'rgba(0, 0, 0, 0.7)',
                                    color: 'white',
                                    padding: '4px 8px',
                                    fontSize: '12px',
                                    borderRadius: '4px',
                                    textAlign: 'center',
                                    wordBreak: 'break-word'
                                }}
                            >
                                {alt}
                            </div>
                        )}
                    </div>
                </>
            )}
        </>
    );
};

export default ImageHoverPreview;
