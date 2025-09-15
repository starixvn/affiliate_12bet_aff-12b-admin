import React, { useState, useCallback, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Input, FormFeedback, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import { updateModuleComponent, fetchModuleSources } from '../../actions';
import { defaultModuleIds } from '../../../libs/constants';

const ModuleSelector = ({ moduleId, value, error, moduleList, moduleComponents, updateModuleComponent, fetchModuleSources }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [selectedModuleId, setSelectedModuleId] = useState(null);

    const toggle = useCallback(() => {
        setIsOpen(prevState => !prevState);
    }, []);

    // Effect to handle module selection and force dropdown close
    useEffect(() => {
        if (selectedModuleId !== null) {
            // Close dropdown immediately
            setIsOpen(false);
            
            // Use setTimeout to ensure state updates happen after dropdown closes
            setTimeout(() => {
                updateModuleComponent(moduleId, 'moduleId', selectedModuleId);
                
                if (selectedModuleId === defaultModuleIds.LoginBtn) {
                    updateModuleComponent(moduleId, 'extraInfo', "/login");
                }
                // Reset source when module changes
                updateModuleComponent(moduleId, 'mediaId', null);
                
                // Fetch sources for the selected module
                if (selectedModuleId) {
                    fetchModuleSources(selectedModuleId);
                }
                
                // Reset selection trigger
                setSelectedModuleId(null);
            }, 10);
        }
    }, [selectedModuleId, updateModuleComponent, moduleId, fetchModuleSources]);

    const handleModuleSelect = useCallback((moduleIdToSelect) => {
        setSelectedModuleId(moduleIdToSelect);
    }, []);


    const handleSearchChange = useCallback((e) => {
        setSearchTerm(e.target.value);
    }, []);

    const filteredModules = moduleList.filter(module => {
        // Filter by search term
        if (!module.ModuleName || !module.ModuleName.toLowerCase().includes(searchTerm.toLowerCase())) {
            return false;
        }
        
        // If module is unique and already used by another component, exclude it
        if (module.IsUnique) {
            const isAlreadyUsed = moduleComponents.some(comp => 
                comp.moduleId === module.ModuleId && comp.id !== moduleId
            );
            if (isAlreadyUsed) {
                return false;
            }
        }
        
        return true;
    });

    const selectedModule = moduleList.find(m => m.ModuleId === value);
    const displayText = selectedModule ? selectedModule.ModuleName : 'Please select...';

    return (
        <div className="module-selector">
            <Dropdown isOpen={isOpen} toggle={toggle} className="w-100">
                <DropdownToggle
                    caret
                    className={`form-control text-left ${error ? 'is-invalid border-danger' : ''}`}
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: 'black' }}
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
                                key={module.ModuleId}
                                tag="button"
                                type="button"
                                onClick={(e) => {
                                    e.preventDefault();
                                    e.stopPropagation();
                                    handleModuleSelect(module.ModuleId);
                                }}
                                active={module.ModuleId === value}
                            >
                                {module.ModuleName}
                            </DropdownItem>
                        ))
                    ) : (
                        <DropdownItem disabled>
                            No modules found
                        </DropdownItem>
                    )}
                </DropdownMenu>
            </Dropdown>
            {/* {error && <div className="invalid-feedback d-block">{error}</div>} */}
        </div>
    );
};

function mapStateToProps({ moduleList, moduleComponents }) {
    return Object.assign({}, { moduleList, moduleComponents });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateModuleComponent, fetchModuleSources }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ModuleSelector));
