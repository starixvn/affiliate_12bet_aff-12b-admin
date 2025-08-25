import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

export default class ModuleManagementApp extends React.PureComponent {
    constructor(props) {
        super(props);

        const { 
            moduleTypes, 
            navigationTargets, 
            existingModules, 
            permissions, 
            totalModules 
        } = window.initialState;

        this.store = configureStore({
            moduleTypes: moduleTypes || [],
            navigationTargets: navigationTargets || [],
            existingModules: existingModules || [],
            permissions: permissions || { canCreate: false, canEdit: false, canDelete: false },
            totalModules: totalModules || 0,
            selectedModule: null,
            isShowLoader: false,
            isShowModal: false,
            modalType: '', // 'create', 'edit', 'delete'
            gridData: existingModules || [],
            checkedModules: {},
            shouldResetScrollPosition: false
        }, reducer);
    }

    render() {
        return (
            <Provider store={this.store}>
                <AppContainer />
            </Provider>
        );
    }
}

// Auto-render if DOM element exists
if (document.getElementById('module-management-root')) {
    import('react-dom').then(({ render }) => {
        render(<ModuleManagementApp />, document.getElementById('module-management-root'));
    });
}
