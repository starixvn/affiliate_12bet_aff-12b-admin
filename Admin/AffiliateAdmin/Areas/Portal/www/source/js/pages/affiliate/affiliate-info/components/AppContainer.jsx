import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import CommissionGroupPopup from '../../../common/components/CommissionGroupPopup';
import ChangePasswordForm from '../../../common/components/ChangePasswordForm';
import Loader from '../../../../components/Loader';
import { setShowPopupPassword, setShowPopupCommGroup, setLanguageList, setCommGroupList } from '../actions'

import { convertArrayToObjectProps } from '../../../../modules/utils';
import { post } from '../../../../modules/ajaxRequest';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.fetchFilter();
    }

    componentDidUpdate(prevProps) {
        const { languageList, commGroupList } = this.props;

        if (this.isEmptyObject(languageList) && this.isEmptyObject(commGroupList)) {
            this.fetchFilter();
        }
    }

    isEmptyObject = (initialObject) => {
        return Object.keys(initialObject).length === 0;
    }

    fetchFilter = async () => {
        const url = 'Portal/Affiliate/GetFilter'
        const response = await post(url);

        const data = response && response.status === 200 ? response.data : null;

        if (!!data) {
            const commGroupList = convertArrayToObjectProps(data.CommGroupList, 'Id', 'Name');
            const languageList = convertArrayToObjectProps(data.LanguageList, 'LanguageCode', 'LanguageName');
            this.props.setLanguageList(languageList);
            this.props.setCommGroupList(commGroupList);
        }
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

function mapStateToProps({ isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup, languageList, commGroupList }) {
    return Object.assign({}, { isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup, languageList, commGroupList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupPassword, setShowPopupCommGroup, setLanguageList, setCommGroupList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer)