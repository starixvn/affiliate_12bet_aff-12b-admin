import React from 'react';
import { connect } from 'react-redux';
import { Row, Col, Alert } from 'reactstrap';

import ModuleRow from './ModuleRow';
import { pageBuilderLabels, validationMessages } from '../../libs/constants';

const ModuleComponentsSection = ({ moduleComponents, validation }) => {

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
                    <strong>{pageBuilderLabels.orderId}</strong>
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

            {/* Add buttons are now integrated into each ModuleSelector */}

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

export default connect(mapStateToProps)(React.memo(ModuleComponentsSection));
