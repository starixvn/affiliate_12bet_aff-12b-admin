import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col, Button } from 'reactstrap';

import ModuleSelector from './ModuleSelector';
import SourceSelector from './SourceSelector';
import { removeModuleComponent } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const ModuleRow = ({ module, isFirst, isLast, canRemove, validation, removeModuleComponent }) => {
    const handleRemove = useCallback(() => {
        if (canRemove) {
            removeModuleComponent(module.id);
        }
    }, [canRemove, removeModuleComponent, module.id]);

    const getModuleError = useCallback(() => {
        return validation.errors[`module_${module.id}`];
    }, [validation.errors, module.id]);

    const getSourceError = useCallback(() => {
        return validation.errors[`source_${module.id}`];
    }, [validation.errors, module.id]);

    return (
        <Row className="mb-3 align-items-start module-row">
            {/* Order */}
            <Col sm={1} className="text-center">
                <div className="module-order">
                    <span className="badge badge-secondary">
                        {module.order}
                    </span>
                </div>
            </Col>

            {/* Module Selector */}
            <Col sm={4}>
                <ModuleSelector 
                    moduleId={module.id}
                    value={module.moduleId}
                    error={getModuleError()}
                />
            </Col>

            {/* Source Selector */}
            <Col sm={4}>
                <SourceSelector 
                    moduleId={module.id}
                    selectedModuleId={module.moduleId}
                    value={module.sourceId}
                    error={getSourceError()}
                />
            </Col>

            {/* Actions */}
            <Col sm={3} className="text-center">
                <div className="module-actions">
                    {canRemove && (
                        <Button
                            color="danger"
                            outline
                            size="sm"
                            onClick={handleRemove}
                            title="Remove this module"
                        >
                            <i className="fa fa-trash"></i>
                            <span className="d-none d-md-inline ms-1">
                                {pageBuilderLabels.remove}
                            </span>
                        </Button>
                    )}
                    
                    {!canRemove && (
                        <small className="text-muted">
                            At least one module required
                        </small>
                    )}
                </div>
            </Col>
        </Row>
    );
};

function mapStateToProps({ validation }) {
    return Object.assign({}, { validation });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ removeModuleComponent }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ModuleRow));
