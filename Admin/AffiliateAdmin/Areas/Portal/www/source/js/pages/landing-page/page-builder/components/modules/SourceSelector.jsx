import React, { useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Input, FormFeedback } from 'reactstrap';

import { updateModuleComponent, fetchModuleSources } from '../../actions';

const SourceSelector = ({ moduleId, selectedModuleId, value, error, sources, updateModuleComponent, fetchModuleSources }) => {
    useEffect(() => {
        // Fetch sources when selectedModuleId changes
        if (selectedModuleId) {
            fetchModuleSources(selectedModuleId);
        }
    }, [selectedModuleId, fetchModuleSources]);

    const handleSourceSelect = useCallback((event) => {
        const sourceId = event.target.value || null;
        updateModuleComponent(moduleId, 'sourceId', sourceId);
    }, [updateModuleComponent, moduleId]);

    const isDisabled = !selectedModuleId;

    return (
        <div className="source-selector">
            <Input
                type="select"
                value={value || ''}
                onChange={handleSourceSelect}
                invalid={!!error}
                disabled={isDisabled}
            >
                <option value="">
                    {isDisabled ? 'Please select module first' : 'Please select...'}
                </option>
                {sources && sources.map(source => (
                    <option key={source.value} value={source.value}>
                        {source.label}
                    </option>
                ))}
            </Input>
            {error && <FormFeedback>{error}</FormFeedback>}
            {isDisabled && (
                <small className="text-muted">
                    Select a module to view available sources
                </small>
            )}
        </div>
    );
};

function mapStateToProps({ sources }) {
    return Object.assign({}, { sources });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateModuleComponent, fetchModuleSources }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(SourceSelector));
