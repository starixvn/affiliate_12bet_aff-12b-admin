import React from 'react';
import { headerText } from '../libs/constants';

const ModuleList = ({ 
    modules, 
    checkedModules, 
    onEditModule, 
    onCheckModule, 
    permissions 
}) => {
    const handleCheckboxChange = (moduleId, checked) => {
        const newCheckedModules = { ...checkedModules };
        newCheckedModules[moduleId] = checked;
        onCheckModule(newCheckedModules);
    };

    const handleCheckAll = (checked) => {
        const newCheckedModules = {};
        modules.forEach(module => {
            newCheckedModules[module.ModuleId] = checked;
        });
        onCheckModule(newCheckedModules);
    };

    const getModuleTypeIcon = (moduleType) => {
        switch (moduleType) {
            case 1: return '🖼️'; // Image
            case 2: return '📞'; // Contact
            case 3: return '📝'; // Form
            case 4: return '🔗'; // Link
            default: return '📄';
        }
    };

    const getModuleTypeName = (moduleType) => {
        switch (moduleType) {
            case 1: return 'Image';
            case 2: return 'Contact';
            case 3: return 'Form';
            case 4: return 'Link';
            default: return 'Unknown';
        }
    };

    const getNavigationName = (navigateTo) => {
        switch (navigateTo) {
            case 0: return 'None';
            case 9: return 'Registration Form';
            case 10: return 'Login Form';
            case 11: return 'Contact Page';
            default: return 'Unknown';
        }
    };

    const allChecked = modules.length > 0 && modules.every(module => checkedModules[module.ModuleId]);
    const someChecked = modules.some(module => checkedModules[module.ModuleId]);

    return (
        <div className="module-list-container">
            <div className="table-responsive">
                <table className="table table-striped table-hover">
                    <thead className="thead-dark">
                        <tr>
                            <th width="40">
                                <input
                                    type="checkbox"
                                    checked={allChecked}
                                    ref={input => {
                                        if (input) input.indeterminate = someChecked && !allChecked;
                                    }}
                                    onChange={(e) => handleCheckAll(e.target.checked)}
                                />
                            </th>
                            <th width="50">{headerText.Index}</th>
                            <th>{headerText.ModuleName}</th>
                            <th width="120">{headerText.ModuleType}</th>
                            <th>{headerText.Remark}</th>
                            <th width="120">{headerText.NavigateTo}</th>
                            <th width="80">{headerText.IsRequired}</th>
                            <th width="80">{headerText.IsUnique}</th>
                            <th width="100">{headerText.IsMultipleUpload}</th>
                            <th width="120">{headerText.CreatedDate}</th>
                            <th width="100">{headerText.Actions}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {modules.length === 0 ? (
                            <tr>
                                <td colSpan="11" className="text-center text-muted py-4">
                                    <div>
                                        <i className="fas fa-inbox fa-2x mb-2"></i>
                                        <p>No modules found. Create your first module to get started.</p>
                                    </div>
                                </td>
                            </tr>
                        ) : (
                            modules.map((module, index) => (
                                <tr key={module.ModuleId} className={checkedModules[module.ModuleId] ? 'table-active' : ''}>
                                    <td>
                                        <input
                                            type="checkbox"
                                            checked={checkedModules[module.ModuleId] || false}
                                            onChange={(e) => handleCheckboxChange(module.ModuleId, e.target.checked)}
                                        />
                                    </td>
                                    <td>{index + 1}</td>
                                    <td>
                                        <div className="d-flex align-items-center">
                                            <span className="module-icon me-2">
                                                {getModuleTypeIcon(module.ModuleType)}
                                            </span>
                                            <strong>{module.ModuleName}</strong>
                                        </div>
                                    </td>
                                    <td>
                                        <span className={`badge badge-${getModuleTypeColor(module.ModuleType)}`}>
                                            {getModuleTypeName(module.ModuleType)}
                                        </span>
                                    </td>
                                    <td>
                                        <span title={module.Remark} className="text-truncate d-inline-block" style={{ maxWidth: '200px' }}>
                                            {module.Remark || '-'}
                                        </span>
                                    </td>
                                    <td>{getNavigationName(module.NavigateTo)}</td>
                                    <td>
                                        <span className={`badge ${module.IsRequired ? 'badge-danger' : 'badge-secondary'}`}>
                                            {module.IsRequired ? 'Yes' : 'No'}
                                        </span>
                                    </td>
                                    <td>
                                        <span className={`badge ${module.IsUnique ? 'badge-warning' : 'badge-secondary'}`}>
                                            {module.IsUnique ? 'Yes' : 'No'}
                                        </span>
                                    </td>
                                    <td>
                                        <span className={`badge ${module.IsMultipleUpload ? 'badge-info' : 'badge-secondary'}`}>
                                            {module.IsMultipleUpload ? 'Yes' : 'No'}
                                        </span>
                                    </td>
                                    <td>
                                        <small className="text-muted">
                                            {new Date(module.CreatedDate).toLocaleDateString()}
                                        </small>
                                    </td>
                                    <td>
                                        <div className="btn-group" role="group">
                                            {permissions.canEdit && (
                                                <button
                                                    type="button"
                                                    className="btn btn-sm btn-outline-primary"
                                                    onClick={() => onEditModule(module)}
                                                    title="Edit Module"
                                                >
                                                    <i className="fas fa-edit"></i>
                                                </button>
                                            )}
                                            <button
                                                type="button"
                                                className="btn btn-sm btn-outline-info"
                                                onClick={() => window.open(`/Portal/ModuleMediaGallery?moduleId=${module.ModuleId}`, '_blank')}
                                                title="Manage Media"
                                            >
                                                <i className="fas fa-images"></i>
                                            </button>
                                        </div>
                                    </td>
                                </tr>
                            ))
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

const getModuleTypeColor = (moduleType) => {
    switch (moduleType) {
        case 1: return 'primary'; // Image
        case 2: return 'success'; // Contact
        case 3: return 'warning'; // Form
        case 4: return 'info'; // Link
        default: return 'secondary';
    }
};

export default ModuleList;
