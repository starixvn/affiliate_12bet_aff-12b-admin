import React, { useState, useEffect } from 'react';
import { Card, CardHeader, CardBody, Button, Row, Col, Alert } from 'reactstrap';
import { MediaPicker, MediaAssignment } from '../components';
import { pageBuilderService } from '../libs/pageBuilderService';

/**
 * Example: Page Builder Integration with Asset Uploader
 * 
 * This example demonstrates how to integrate the Asset Uploader components
 * into a page builder interface for managing media assignments to modules.
 */
const PageBuilderIntegration = ({ pageId = 1 }) => {
    const [modules, setModules] = useState([]);
    const [selectedModule, setSelectedModule] = useState(null);
    const [showMediaPicker, setShowMediaPicker] = useState(false);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    // Mock modules data - in real implementation, this would come from the API
    useEffect(() => {
        const mockModules = [
            {
                ModuleId: 1,
                ModuleName: 'Hero Section Background',
                ModuleType: 1, // Image
                IsRequired: true,
                IsMultipleUpload: false,
                MaxAssignments: 1
            },
            {
                ModuleId: 2,
                ModuleName: 'Logo & Sponsor Images',
                ModuleType: 1, // Image
                IsRequired: false,
                IsMultipleUpload: true,
                MaxAssignments: 5
            },
            {
                ModuleId: 3,
                ModuleName: 'Contact Form Background',
                ModuleType: 1, // Image
                IsRequired: false,
                IsMultipleUpload: false,
                MaxAssignments: 1
            },
            {
                ModuleId: 4,
                ModuleName: 'CTA Button Images',
                ModuleType: 1, // Image
                IsRequired: false,
                IsMultipleUpload: true,
                MaxAssignments: 3
            }
        ];
        setModules(mockModules);
    }, []);

    const handleMediaAssigned = (media, moduleId) => {
        console.log('Media assigned:', media, 'to module:', moduleId);
        // Here you would typically refresh the page builder preview
        // or update the module's visual representation
    };

    const handleMediaRemoved = (mediaId, moduleId) => {
        console.log('Media removed:', mediaId, 'from module:', moduleId);
        // Here you would typically refresh the page builder preview
        // or update the module's visual representation
    };

    const handleQuickMediaSelect = async (selectedMedia) => {
        if (!selectedModule || !selectedMedia) return;

        try {
            setLoading(true);
            setError(null);

            const param = {
                pageId: pageId,
                moduleId: selectedModule.ModuleId,
                mediaId: selectedMedia.MediaId,
                mediaRole: 'primary',
                displayOrder: 1,
                isActive: true
            };

            const result = await pageBuilderService.assignMediaToModule(param);
            if (result && result.IsSuccessful) {
                handleMediaAssigned(selectedMedia, selectedModule.ModuleId);
                setShowMediaPicker(false);
                setSelectedModule(null);
            } else {
                setError(result?.Message || 'Failed to assign media');
            }
        } catch (err) {
            console.error('Error in quick media select:', err);
            setError('Error assigning media: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="page-builder-integration">
            <div className="page-builder-header mb-4">
                <h3>
                    <i className="oi oi-layers mr-2"></i>
                    Landing Page Builder - Media Management
                </h3>
                <p className="text-muted">
                    Assign media files to different modules in your landing page. 
                    Each module can have specific media requirements and limitations.
                </p>
            </div>

            {/* Error Display */}
            {error && (
                <Alert color="danger" className="mb-4">
                    {error}
                    <Button 
                        size="sm" 
                        color="outline-danger" 
                        className="float-right"
                        onClick={() => setError(null)}
                    >
                        ×
                    </Button>
                </Alert>
            )}

            {/* Module Media Assignments */}
            <Row>
                {modules.map((module) => (
                    <Col key={module.ModuleId} lg={6} className="mb-4">
                        <MediaAssignment
                            moduleId={module.ModuleId}
                            moduleType={module.ModuleType}
                            pageId={pageId}
                            moduleName={module.ModuleName}
                            onMediaAssigned={handleMediaAssigned}
                            onMediaRemoved={handleMediaRemoved}
                            allowMultiple={module.IsMultipleUpload}
                            maxAssignments={module.MaxAssignments}
                        />
                    </Col>
                ))}
            </Row>

            {/* Quick Actions Panel */}
            <Card className="mt-4">
                <CardHeader>
                    <h5 className="mb-0">
                        <i className="oi oi-flash mr-2"></i>
                        Quick Actions
                    </h5>
                </CardHeader>
                <CardBody>
                    <div className="d-flex flex-wrap gap-2">
                        {modules.map((module) => (
                            <Button
                                key={`quick-${module.ModuleId}`}
                                size="sm"
                                color="outline-primary"
                                onClick={() => {
                                    setSelectedModule(module);
                                    setShowMediaPicker(true);
                                }}
                                disabled={loading}
                            >
                                <i className="oi oi-plus mr-1"></i>
                                Add to {module.ModuleName}
                            </Button>
                        ))}
                    </div>
                    
                    <div className="mt-3">
                        <small className="text-muted">
                            <i className="oi oi-info"></i>
                            <strong>Tip:</strong> Use quick actions to rapidly assign media to modules, 
                            or use the individual module cards above for more detailed management.
                        </small>
                    </div>
                </CardBody>
            </Card>

            {/* Global Media Picker */}
            <MediaPicker
                show={showMediaPicker}
                onHide={() => {
                    setShowMediaPicker(false);
                    setSelectedModule(null);
                }}
                onSelectMedia={handleQuickMediaSelect}
                moduleId={selectedModule?.ModuleId}
                moduleType={selectedModule?.ModuleType}
                allowMultiple={false} // Quick actions are single-select
                title={selectedModule ? `Select Media for ${selectedModule.ModuleName}` : 'Select Media'}
            />

            {/* Integration Notes */}
            <Card className="mt-4 border-info">
                <CardHeader className="bg-info text-white">
                    <h6 className="mb-0">
                        <i className="oi oi-code mr-2"></i>
                        Integration Notes
                    </h6>
                </CardHeader>
                <CardBody>
                    <h6>Available Components:</h6>
                    <ul className="small">
                        <li><strong>MediaPicker:</strong> Modal for selecting media files with filtering and pagination</li>
                        <li><strong>MediaAssignment:</strong> Complete media management for a specific module</li>
                        <li><strong>AssetUploaderContainer:</strong> Full asset uploader interface</li>
                    </ul>

                    <h6 className="mt-3">Key Features:</h6>
                    <ul className="small">
                        <li>Module-specific media validation and constraints</li>
                        <li>Drag & drop file uploads with progress tracking</li>
                        <li>Advanced filtering and search capabilities</li>
                        <li>Bulk operations for efficient media management</li>
                        <li>Real-time preview and thumbnail generation</li>
                        <li>Integration with existing page builder workflows</li>
                    </ul>

                    <h6 className="mt-3">Usage in Page Builder:</h6>
                    <pre className="small bg-light p-2 rounded">
{`// Import components
import { MediaPicker, MediaAssignment } from '../asset-uploader/components';

// Use in your page builder
<MediaAssignment
    moduleId={moduleId}
    moduleType={moduleType}
    pageId={pageId}
    moduleName="Hero Background"
    onMediaAssigned={(media) => updatePreview()}
    allowMultiple={false}
    maxAssignments={1}
/>`}
                    </pre>
                </CardBody>
            </Card>
        </div>
    );
};

export default PageBuilderIntegration;
