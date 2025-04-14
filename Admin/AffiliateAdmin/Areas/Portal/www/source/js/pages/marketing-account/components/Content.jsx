import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import { toggleLoader } from '../../../actions/loaderAction';
import { setIsShowAddAffMktAccountPopup, setAffId } from '../actions';
import { allStatuses, statusList } from '../libs/constants';

import ReportTitle from '../../../components/ReportTitle';
import GridData from './GridData';

export const Content = (props) => {
    const { param, canAddAffMktAccount } = props;

    const isShowCreateAffMktAccBtn = param && canAddAffMktAccount;
    const [title, setTitle] = useState('');

    useEffect(() => {
        setTitle(_buildTitleString());
    }, [JSON.stringify(param)]);

    const _buildTitleString = () => {
        if (!param) return '';

        const { languageCodeList, targetCountryCodeList } = props;

        const language = languageCodeList[param.languageCode] || 'All Languages';
        const targetCountry = targetCountryCodeList[param.targetCountryCode] || 'All Target Countries';
        const status = statusList[param.status] || statusList[allStatuses];

        return `MARKETING ACCOUNT REPORT - ${targetCountry.toUpperCase()} - ${language.toUpperCase()} - ${status.toUpperCase()}`;
    };

    const _renderReportTitle = () => {
        return <div className="d-flex align-items-center">
            {title}
        </div>
    };

    const _handleAddNewAccount = () => {
        props.setIsShowAddAffMktAccountPopup(true);
        props.setAffId(927841);
    }

    return (
        <div className="data-content my-2">
            {isShowCreateAffMktAccBtn && <div className="my-2">
                <Button outline type="button" onClick={() => _handleAddNewAccount()}>
                    Add New Account
                    <span className="oi oi-plus ml-2"></span>
                </Button>
            </div>}
            {title && <ReportTitle content={_renderReportTitle()} />}
            <GridData />
        </div>
    )
}

const mapStateToProps = ({ param, languageCodeList, targetCountryCodeList, canAddAffMktAccount }) => {
    return {
        param,
        languageCodeList,
        targetCountryCodeList,
        canAddAffMktAccount
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        toggleLoader,
        setIsShowAddAffMktAccountPopup,
        setAffId
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));