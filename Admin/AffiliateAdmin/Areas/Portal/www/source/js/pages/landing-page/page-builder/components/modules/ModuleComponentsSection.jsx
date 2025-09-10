import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col, Button, Alert } from 'reactstrap';

import ModuleRow from './ModuleRow';
import { addModuleComponent } from '../../actions';
import { pageBuilderLabels, validationMessages } from '../../libs/constants';

const ModuleComponentsSection = ({ moduleComponents, validation, addModuleComponent }) => {
    const handleAddModule = useCallback(() => {
        addModuleComponent();
    }, [addModuleComponent]);

    const hasModuleErrors = validation.errors.moduleComponents;

    return (
        <div className="module-components-section">
            {hasModuleErrors && (
                <Row className="mb-3">
                    <Col>
                        <Alert color="danger">
                            {hasModuleErrors}
                        </Alert>
                    </Col>
                </Row>
            )}

            {/* Header Row */}
            <Row className="mb-2">
                <Col sm={1} className="text-center">
                    <strong>{pageBuilderLabels.order}</strong>
                </Col>
                <Col sm={4}>
                    <strong>{pageBuilderLabels.module}</strong>
                </Col>
                <Col sm={4}>
                    <strong>{pageBuilderLabels.source}</strong>
                </Col>
                <Col sm={3} className="text-center">
                    <strong>Actions</strong>
                </Col>
            </Row>

            {/* Module Rows */}
            {moduleComponents && moduleComponents.map((module, index) => (
                <ModuleRow 
                    key={module.id}
                    module={module}
                    isFirst={index === 0}
                    isLast={index === moduleComponents.length - 1}
                    canRemove={moduleComponents.length > 1}
                />
            ))}

            {/* Add Module Button */}
            <Row className="mt-3">
                <Col>
                    <Button 
                        color="primary" 
                        outline 
                        onClick={handleAddModule}
                        size="sm"
                    >
                        <i className="fa fa-plus me-1"></i>
                        {pageBuilderLabels.add}
                    </Button>
                </Col>
            </Row>

            {moduleComponents && moduleComponents.length === 0 && (
                <Row className="mt-3">
                    <Col>
                        <Alert color="warning">
                            {validationMessages.atLeastOneModule}
                        </Alert>
                    </Col>
                </Row>
            )}
        </div>
    );
};

function mapStateToProps({ moduleComponents, validation }) {
    return Object.assign({}, { moduleComponents, validation });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ addModuleComponent }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ModuleComponentsSection));
