import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import CommissionGroupPopup from '../../../common/components/CommissionGroupPopup';
import ChangePasswordForm from '../../../common/components/ChangePasswordForm';
import Loader from '../../../../components/Loader';
import { setShowPopupPassword, setShowPopupCommGroup, setLanguageList, setCommGroupList, setTargetCountryList } from '../actions'

import { convertArrayToObjectProps } from '../../../../modules/utils';
import { post } from '../../../../modules/ajaxRequest';
import { claimResource } from '../../../../modules/claim';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.fetchFilter();
    }

    componentDidUpdate(prevProps) {
        const { languageList, commGroupList, targetCountryList } = this.props;

        if (this.isEmptyObject(languageList) && this.isEmptyObject(commGroupList) && this.isEmptyObject(targetCountryList)) {
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
            const targetCountryList = convertArrayToObjectProps(data.TargetCountryList, 'TargetCountryCode', 'TargetCountryName');
            this.props.setLanguageList(languageList);
            this.props.setCommGroupList(commGroupList);
            this.props.setTargetCountryList(targetCountryList);
        }
    }

    render() {
        const { isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup, isCommGroupVisible, isPwdChangeable } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter />
                <Content />
                {
                    isCommGroupVisible && <CommissionGroupPopup
                        showPopupCommGroup={showPopupCommGroup}
                        isPrivateGroup={true}
                        isCommGroupVisible={true}
                        affId={affId}
                        affName={commGroup}
                        setShowPopupCommGroup={this.props.setShowPopupCommGroup}
                    />
                }
                {
                    isPwdChangeable && <ChangePasswordForm
                        showPopupPassword={showPopupPassword}
                        selectedId={affId}
                        setShowPopupPassword={this.props.setShowPopupPassword}
                    />
                }
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup, languageList, commGroupList, targetCountryList, claimsPrincipal }) {
    const isCommGroupVisible = claimsPrincipal.hasReadClaim(claimResource.CommissionGroup);
    const isPwdChangeable = claimsPrincipal.hasWriteClaim(claimResource.AffPassword);

    return Object.assign({}, { isShowLoader, showPopupPassword, affId, commGroup, showPopupCommGroup, languageList, commGroupList, targetCountryList, isCommGroupVisible, isPwdChangeable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupPassword, setShowPopupCommGroup, setLanguageList, setCommGroupList, setTargetCountryList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer)