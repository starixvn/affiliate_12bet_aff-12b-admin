import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';
import { Col, FormGroup, Label } from 'reactstrap';
import { targetCountryStatus } from '../../../modules/constants';
import { getExcludedTargetCountries } from '../libs/utils/targetcountry';

export default class ExcludedTargetCountryFormGroup extends PureComponent {
    constructor(props) {
        super(props);
    }

    _handleChangeTargetCountry = (event, targetCountryCode) => {
        const isChecked = event.target.checked;

        const { usingTargetCountries, targetCountryList } = this.props;

        if (usingTargetCountries.includes(targetCountryCode)) {
            return;
        }

        const newTargetCountryList = targetCountryList.map(item => {
            if (item.TargetCountryCode !== targetCountryCode) {
                return item;
            }
            return {
                ...item,
                Status: isChecked ? targetCountryStatus.Excluded : targetCountryStatus.Included
            };
        });

        const excludedTargetCountries = getExcludedTargetCountries(newTargetCountryList);
        const isExcludedAllTargetCountries = (usingTargetCountries.length + excludedTargetCountries.length) === newTargetCountryList.length;

        this.props.changeTargetCountry(isExcludedAllTargetCountries, excludedTargetCountries, newTargetCountryList);
    }

    _handleChangeAllTargetCountries = (event) => {
        const isChecked = event.target.checked;
        const { targetCountryList } = this.props;

        const status = isChecked ? targetCountryStatus.Excluded : targetCountryStatus.Included;
        const newTargetCountryList = targetCountryList.map(item => {
            if (item.Status === targetCountryStatus.Using) {
                return item;
            }
            return {
                ...item,
                Status: status
            };
        });

        const excludedTargetCountries = getExcludedTargetCountries(newTargetCountryList);

        this.props.changeTargetCountry(isChecked, excludedTargetCountries, newTargetCountryList);
    }

    render() {
        const { lblText, targetCountryList, isExcludedAllTargetCountries, usingTargetCountries, sm } = this.props;

        return targetCountryList && (
            <FormGroup row>
                <Label sm={sm}>{lblText}</Label>
                <Col>
                    <div className="excluded-target-country">
                        {
                            targetCountryList.map(item => {
                                const isUsing = usingTargetCountries.includes(item.TargetCountryCode);

                                return (
                                    <div key={item.TargetCountryCode} className="excluded-target-country-item">
                                        <input type="checkbox" className="mr-2" disabled={isUsing} title={isUsing && "This Affiliate domain has been used for this target country!"} id={item.TargetCountryCode} checked={item.Status === targetCountryStatus.Excluded} onChange={(e) => this._handleChangeTargetCountry(e, item.TargetCountryCode)} />
                                        <Label for={item.TargetCountryCode}>{item.TargetCountryName}</Label>
                                    </div>
                                )
                            })
                        }
                        <div className="excluded-target-country-item excluded-target-country-All">
                            <input type="checkbox" className="mr-2" disabled={targetCountryList.every(i => i.Status === targetCountryStatus.Using)} id="All-Target-Countries" checked={isExcludedAllTargetCountries} onChange={this._handleChangeAllTargetCountries} />
                            <Label for="All-Target-Countries">All</Label>
                        </div>
                    </div>
                </Col>
            </FormGroup>
        )
    }
}

ExcludedTargetCountryFormGroup.propTypes = {
    sm: PropTypes.number,
    targetCountryList: PropTypes.array.isRequired,
    usingTargetCountries: PropTypes.array.isRequired,
    isExcludedAllTargetCountries: PropTypes.bool,
    changeTargetCountry: PropTypes.func.isRequired,
    tooltipText: PropTypes.string,
    lblText: PropTypes.string
};

ExcludedTargetCountryFormGroup.defaultProps = {
    sm: 3,
    targetCountryList: null,
    isExcludedAllTargetCountries: false,
    usingTargetCountries: [],
    tooltipText: 'This Affiliate domain has been used for this target country!',
    lblText: 'Excluded Target Country'
};