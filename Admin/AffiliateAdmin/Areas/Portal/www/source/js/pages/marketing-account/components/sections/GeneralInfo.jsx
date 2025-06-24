import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { FormGroup, Label, Input, Col } from 'reactstrap';

import { setAffId } from '../../actions';
import Field from '../../../../components/Field';
import RowField from './RowField';

export const GeneralInfo = (props) => {
    const { generalInfo, setGeneralInfo } = props;
    console.log('generalInfo', generalInfo)
    const onChange = (e) => {
        setGeneralInfo({
            ...generalInfo,
            [e.target.name]: e.target.value
        });
    }

    const options = [
        {
            label: '12BETAFF001',
            value: 927841
        },
        {
            label: '12BETAFF12606',
            value: 927841
        },
        {
            label: '12BETAFF16517',
            value: 92043606
        },
        {
            label: '12BETAFF16518',
            value: 92043606
        }
    ]

    const targetCountryCodeList = Object.keys(props.targetCountryCodeList).map(key => {
        return {
            label: props.targetCountryCodeList[key],
            value: key
        }
    })

    const languageCodeList = Object.keys(props.languageCodeList).map(key => {
        return {
            label: props.languageCodeList[key],
            value: key
        }
    })

    return (
        <div>
            <RowField
                label="Copy from Marketing account"
                name="marketingAccount"
                value={props.affId}
                onChange={e => props.setAffId(e.target.value)}
                selectOptions={options}
            />
            <RowField
                label="Target Country"
                name="targetCountryCode"
                value={generalInfo.targetCountryCode}
                onChange={onChange}
                selectOptions={targetCountryCodeList}
            />
            <RowField
                label="Language"
                name="language"
                value={generalInfo.language}
                onChange={onChange}
                selectOptions={languageCodeList}
            />
        </div>
    );
};

const mapStateToProps = ({ affId, languageCodeList, targetCountryCodeList }) => {
    return {
        affId,
        languageCodeList,
        targetCountryCodeList
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setAffId
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(GeneralInfo));
