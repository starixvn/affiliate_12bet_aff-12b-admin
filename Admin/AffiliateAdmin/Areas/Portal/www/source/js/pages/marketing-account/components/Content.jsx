import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';
import { toggleLoader } from '../../../actions/loaderAction';
import { setIsShowAddAffMktAccountPopup, setAff } from '../actions';
import GridData from './GridData';

export const Content = (props) => {
    const { params, canAddAffMktAccount } = props;
    const isShowCreateAffMktAccBtn = params && canAddAffMktAccount;

    const _handleAddNewAccount = (affId, affName) => {
        props.setIsShowAddAffMktAccountPopup(true);
        props.setAff({ affId, affName });
    }

    return (
        <div className="data-content my-2">
            {isShowCreateAffMktAccBtn && <div className="my-2">
                <Button outline type="button" onClick={() => _handleAddNewAccount(927841, '12BETAFF001')}>
                    Add New Account
                    <span className="oi oi-plus ml-2"></span>
                </Button>
            </div>}
            <GridData handleAddNewAccount={_handleAddNewAccount} />
        </div>
    )
}

const mapStateToProps = ({ params, languageCodeList, targetCountryCodeList, canAddAffMktAccount }) => {
    return {
        params,
        languageCodeList,
        targetCountryCodeList,
        canAddAffMktAccount
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        toggleLoader,
        setIsShowAddAffMktAccountPopup,
        setAff
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));