import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { setAffId } from '../../actions';
import RowField from './RowField';

export const GeneralInfo = (props) => {
    const { generalInfo, setGeneralInfo } = props;

    console.log('generalInfo', generalInfo);
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

    const dealOptions = Object.keys(props.dealOptionsList).map(key => {
        return {
            label: props.dealOptionsList[key],
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
                name="preferredLanguage"
                value={generalInfo.preferredLanguage}
                onChange={onChange}
                selectOptions={languageCodeList}
            />
            <RowField
                label="Deal Name"
                name="groupId"
                value={generalInfo.groupId}
                onChange={onChange}
                selectOptions={dealOptions}
            />
        </div>
    );
};

const mapStateToProps = ({ affId, languageCodeList, targetCountryCodeList, dealOptionsList }) => {
    return {
        affId,
        languageCodeList,
        targetCountryCodeList,
        dealOptionsList
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setAffId
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(GeneralInfo));
