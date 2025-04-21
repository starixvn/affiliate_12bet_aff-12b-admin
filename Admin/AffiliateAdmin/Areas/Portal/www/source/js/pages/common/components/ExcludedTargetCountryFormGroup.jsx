import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';
import { Col, FormGroup, Label } from 'reactstrap';
import { targetCountryStatus } from '../../../modules/constants';
import { getExcludedTargetCountryList } from '../modules/utils/targetcountry';

export default class ExcludedTargetCountryFormGroup extends PureComponent {
    constructor(props) {
        super(props);
    }

    _handleChangeTargetCountry = (event, targetCountryCode) => {
        const isChecked = event.target.checked;

        const { targetCountryList } = this.props;

        const newTargetCountryList = targetCountryList.map(item => {
            if (item.TargetCountryCode !== targetCountryCode) {
                return item;
            }
            return {
                ...item,
                Status: isChecked ? targetCountryStatus.Excluded : targetCountryStatus.Included
            };
        });

        const excludedTargetCountryList = getExcludedTargetCountryList(newTargetCountryList);
        const isExcludedAllTargetCountries = excludedTargetCountryList.length === newTargetCountryList.length;

        this.props.changeTargetCountry(isExcludedAllTargetCountries, excludedTargetCountryList.join(','), newTargetCountryList);
    }

    _handleChangeAllTargetCountries = (event) => {
        const isChecked = event.target.checked;
        const { targetCountryList } = this.props;

        const status = isChecked ? targetCountryStatus.Excluded : targetCountryStatus.Included;
        const newTargetCountryList = targetCountryList.map(item => {
            return {
                ...item,
                Status: status
            };
        });

        const excludedTargetCountryList = getExcludedTargetCountryList(newTargetCountryList);

        this.props.changeTargetCountry(isChecked, excludedTargetCountryList.join(','), newTargetCountryList);
    }

    render() {
        const { lblText, targetCountryList, isExcludedAllTargetCountries, sm } = this.props;

        return targetCountryList && (
            <FormGroup row>
                <Label sm={sm}>{lblText}</Label>
                <Col>
                    <div className="excluded-target-country">
                        {
                            targetCountryList.map(item => {
                                return (
                                    <div key={item.TargetCountryCode} className="excluded-target-country-item">
                                        <input type="checkbox" className="mr-2" id={item.TargetCountryCode} checked={item.Status === targetCountryStatus.Excluded} onChange={(e) => this._handleChangeTargetCountry(e, item.TargetCountryCode)} />
                                        <Label for={item.TargetCountryCode}>{item.TargetCountryName}</Label>
                                    </div>
                                )
                            })
                        }
                        <div className="excluded-target-country-item excluded-target-country-All">
                            <input type="checkbox" className="mr-2" id="All-Target-Countries" checked={isExcludedAllTargetCountries} onChange={this._handleChangeAllTargetCountries} />
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
    isExcludedAllTargetCountries: PropTypes.bool,
    changeTargetCountry: PropTypes.func.isRequired,
    lblText: PropTypes.string
};

ExcludedTargetCountryFormGroup.defaultProps = {
    sm: 3,
    targetCountryList: null,
    isExcludedAllTargetCountries: false,
    lblText: 'Excluded Target Country'
};