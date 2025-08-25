import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ModuleList from './ModuleList';
import ModuleForm from './ModuleForm';
import ControlButtons from '../../common/components/ControlButtons';

import { messages } from '../libs/constants';
import { 
    fetchModules, 
    toggleLoader, 
    setSelectedModule, 
    setCheckedModules, 
    deleteModules, 
    setShouldResetScrollPosition,
    showModal,
    hideModal
} from '../actions';

export const AppContainer = (props) => {
    const handleCreateModule = () => {
        props.setSelectedModule(null);
        props.showModal('create');
    };

    const handleEditModule = (module) => {
        props.setSelectedModule(module);
        props.showModal('edit');
    };

    const handleDeleteModules = (moduleIds) => {
        props.deleteModules({ moduleIds });
    };

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            
            <div className="module-management-container">
                <div className="page-header">
                    <h2>Landing Page Module Management</h2>
                    <p>Manage reusable modules for landing page builder</p>
                </div>

                <ControlButtons
                    messages={messages}
                    gridData={props.gridData}
                    fetchData={props.fetchModules}
                    checkedItems={props.checkedModules}
                    setCheckedItems={props.setCheckedModules}
                    toggleLoader={props.toggleLoader}
                    setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                    setSelectedItemId={props.setSelectedModule}
                    deleteItems={handleDeleteModules}
                    onCreateNew={handleCreateModule}
                    permissions={props.permissions}
                />

                <ModuleList
                    modules={props.gridData}
                    checkedModules={props.checkedModules}
                    onEditModule={handleEditModule}
                    onCheckModule={props.setCheckedModules}
                    permissions={props.permissions}
                />

                <ModuleForm
                    isVisible={props.isShowModal}
                    modalType={props.modalType}
                    selectedModule={props.selectedModule}
                    moduleTypes={props.moduleTypes}
                    navigationTargets={props.navigationTargets}
                    onClose={props.hideModal}
                    onSubmit={(data) => {
                        // Handle form submission
                        console.log('Form submitted:', data);
                        props.hideModal();
                        props.fetchModules();
                    }}
                />
            </div>
        </AppLayout>
    );
};

function mapStateToProps(state) {
    return {
        isShowLoader: state.isShowLoader,
        gridData: state.gridData,
        checkedModules: state.checkedModules,
        selectedModule: state.selectedModule,
        isShowModal: state.isShowModal,
        modalType: state.modalType,
        moduleTypes: state.moduleTypes,
        navigationTargets: state.navigationTargets,
        permissions: state.permissions
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({
        fetchModules,
        toggleLoader,
        setSelectedModule,
        setCheckedModules,
        deleteModules,
        setShouldResetScrollPosition,
        showModal,
        hideModal
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));
