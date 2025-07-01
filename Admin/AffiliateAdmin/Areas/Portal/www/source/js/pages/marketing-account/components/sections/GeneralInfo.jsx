import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { setAff } from '../../actions';
import RowField from './RowField';

export const GeneralInfo = (props) => {
    const { generalInfo, setGeneralInfo, aff } = props;
    const onChange = (e) => {
        setGeneralInfo({
            ...generalInfo,
            [e.target.name]: e.target.value
        });
    }

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
        <div style={{ paddingLeft: '10px' }}>
            <RowField
                label="Copy from Marketing account"
                name="marketingAccount"
                value={aff.affName}
                onChange={e => props.setAff(e.target.value)}
                disabled={true}
                labelProps={{ style: { paddingRight: '0px' } }}
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

const mapStateToProps = ({ aff, languageCodeList, targetCountryCodeList, dealOptionsList }) => {
    return {
        aff,
        languageCodeList,
        targetCountryCodeList,
        dealOptionsList
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setAff
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(GeneralInfo));
