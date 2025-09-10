import React, { useState, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Input, FormFeedback, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import { updateModuleComponent, fetchModuleSources } from '../../actions';

const ModuleSelector = ({ moduleId, value, error, modules, updateModuleComponent, fetchModuleSources }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');

    const toggle = useCallback(() => {
        setIsOpen(prevState => !prevState);
    }, []);

    const handleModuleSelect = useCallback((selectedModuleId) => {
        updateModuleComponent(moduleId, 'moduleId', selectedModuleId);
        
        // Reset source when module changes
        updateModuleComponent(moduleId, 'sourceId', null);
        
        // Fetch sources for the selected module
        if (selectedModuleId) {
            fetchModuleSources(selectedModuleId);
        }
        
        setIsOpen(false);
    }, [updateModuleComponent, moduleId, fetchModuleSources]);

    const handleSearchChange = useCallback((e) => {
        setSearchTerm(e.target.value);
    }, []);

    const filteredModules = modules.filter(module =>
        module.label && module.label.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const selectedModule = modules.find(m => m.value === value);
    const displayText = selectedModule ? selectedModule.label : 'Please select...';

    return (
        <div className="module-selector">
            <Dropdown isOpen={isOpen} toggle={toggle} className="w-100">
                <DropdownToggle
                    caret
                    className={`form-control text-left ${error ? 'is-invalid' : ''}`}
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da' }}
                >
                    {displayText}
                </DropdownToggle>
                <DropdownMenu className="w-100" style={{ maxHeight: '300px', overflowY: 'auto' }}>
                    <DropdownItem header>
                        <Input
                            type="text"
                            placeholder="Search modules..."
                            value={searchTerm}
                            onChange={handleSearchChange}
                            onClick={(e) => e.stopPropagation()}
                        />
                    </DropdownItem>
                    <DropdownItem divider />
                    {filteredModules.length > 0 ? (
                        filteredModules.map(module => (
                            <DropdownItem
                                key={module.value}
                                onClick={() => handleModuleSelect(module.value)}
                                active={module.value === value}
                            >
                                {module.label}
                            </DropdownItem>
                        ))
                    ) : (
                        <DropdownItem disabled>
                            No modules found
                        </DropdownItem>
                    )}
                </DropdownMenu>
            </Dropdown>
            {error && <div className="invalid-feedback d-block">{error}</div>}
        </div>
    );
};

function mapStateToProps({ modules }) {
    return Object.assign({}, { modules });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateModuleComponent, fetchModuleSources }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ModuleSelector));
