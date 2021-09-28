import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';
// import { setShowPopupPassword } from '../actions'

import { convertArrayToObjectProps } from '../../../../modules/utils';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, affId }) {
    return Object.assign({}, { isShowLoader, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);