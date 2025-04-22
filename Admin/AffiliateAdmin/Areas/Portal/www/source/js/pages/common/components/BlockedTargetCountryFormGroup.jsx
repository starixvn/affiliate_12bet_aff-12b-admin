import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';
import { Col, FormGroup, Label } from 'reactstrap';
import { targetCountryStatus } from '../../../modules/constants';
import { getBlockedTargetCountryList } from '../modules/utils/targetcountry';

export default class BlockedTargetCountryFormGroup extends PureComponent {
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
                Status: isChecked ? targetCountryStatus.Blocked : targetCountryStatus.Included
            };
        });

        const blockedTargetCountryList = getBlockedTargetCountryList(newTargetCountryList);
        const isBlockedAllTargetCountries = blockedTargetCountryList.length === newTargetCountryList.length;

        this.props.changeTargetCountry(isBlockedAllTargetCountries, blockedTargetCountryList.join(','), newTargetCountryList);
    }

    _handleChangeAllTargetCountries = (event) => {
        const isChecked = event.target.checked;
        const { targetCountryList } = this.props;

        const status = isChecked ? targetCountryStatus.Blocked : targetCountryStatus.Included;
        const newTargetCountryList = targetCountryList.map(item => {
            return {
                ...item,
                Status: status
            };
        });

        const blockedTargetCountryList = getBlockedTargetCountryList(newTargetCountryList);

        this.props.changeTargetCountry(isChecked, blockedTargetCountryList.join(','), newTargetCountryList);
    }

    render() {
        const { lblText, targetCountryList, isBlockedAllTargetCountries, sm } = this.props;

        return targetCountryList && (
            <FormGroup row>
                <Label sm={sm}>{lblText}</Label>
                <Col>
                    <div className="blocked-target-country">
                        {
                            targetCountryList.map(item => {
                                return (
                                    <div key={item.TargetCountryCode} className="blocked-target-country-item">
                                        <input type="checkbox" className="mr-2" id={item.TargetCountryCode} checked={item.Status === targetCountryStatus.Blocked} onChange={(e) => this._handleChangeTargetCountry(e, item.TargetCountryCode)} />
                                        <Label for={item.TargetCountryCode}>{item.TargetCountryName}</Label>
                                    </div>
                                )
                            })
                        }
                        <div className="blocked-target-country-item blocked-target-country-All">
                            <input type="checkbox" className="mr-2" id="All-Target-Countries" checked={isBlockedAllTargetCountries} onChange={this._handleChangeAllTargetCountries} />
                            <Label for="All-Target-Countries">All</Label>
                        </div>
                    </div>
                </Col>
            </FormGroup>
        )
    }
}

BlockedTargetCountryFormGroup.propTypes = {
    sm: PropTypes.number,
    targetCountryList: PropTypes.array.isRequired,
    isBlockedAllTargetCountries: PropTypes.bool,
    changeTargetCountry: PropTypes.func.isRequired,
    lblText: PropTypes.string
};

BlockedTargetCountryFormGroup.defaultProps = {
    sm: 3,
    targetCountryList: null,
    isBlockedAllTargetCountries: false,
    lblText: 'Blocked Target Country'
};