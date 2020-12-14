import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import CommissionGroupPopup from '../../common/components/CommissionGroupPopup';
import ChangePasswordForm from '../../common/components/ChangePasswordForm';
import Loader from '../../../components/Loader';
import { setShowPopupPassword, setShowPopupCommGroup , setLanguageList, setCommGroupList} from '../actions'

import { convertArrayToObjectProps } from '../../../modules/utils';
import {post} from '../../../modules/ajaxRequest'

export class AppContainer extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount = async () => {
        const data = await this.fetchFilter();

        if (!!data) {
            const commGroupList = convertArrayToObjectProps(data.CommGroupList, 'Id', 'Name');
            const languageList = convertArrayToObjectProps(data.LanguageList, 'LanguageCode', 'LanguageName');
            this.props.setLanguageList(languageList);
            this.props.setCommGroupList(commGroupList);
        }
    }

    fetchFilter = async () => {
        const url = 'Portal/Affiliate/GetFilter'
        const response = await post(url);

        return response && response.status === 200 ? response.data : null;
    }

    render() {
        const { isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter />
                <Content />
                <CommissionGroupPopup
                    showPopupCommGroup={showPopupCommGroup}
                    isPrivateGroup={true}
                    affId={affId}
                    affName={commGroup}
                    setShowPopupCommGroup={this.props.setShowPopupCommGroup}
                />
                <ChangePasswordForm
                    showPopupPassword={showPopupPassword}
                    selectedId={affId}
                    setShowPopupPassword={this.props.setShowPopupPassword}
                />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup }) {
    return Object.assign({}, { isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupPassword, setShowPopupCommGroup, setLanguageList, setCommGroupList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);