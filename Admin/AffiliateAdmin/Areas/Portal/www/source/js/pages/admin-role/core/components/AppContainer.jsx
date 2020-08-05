import React, { Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import { setSelectedMenu } from '../actions';
import { adminRoleMenuList } from '../libs/menu';

export const AppContainer = (props) => {
    const { isShowLoader, selectedMenu } = props;

    const componentIndex = adminRoleMenuList.findIndex(p => p.key === selectedMenu);
    const ComponentItem = componentIndex === -1 ? null : adminRoleMenuList[componentIndex].component;

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <div class="admin-role-content-container">
                {
                    componentIndex !== -1 &&
                    <Suspense fallback={null}>
                        <ComponentItem />
                    </Suspense>
                }
            </div>
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, selectedMenu }) {
    return Object.assign({}, { isShowLoader, selectedMenu });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMenu }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));