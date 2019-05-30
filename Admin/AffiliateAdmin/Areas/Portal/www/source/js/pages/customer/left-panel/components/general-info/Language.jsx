import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Input, Button } from 'reactstrap';

import { getAffiliateInfo } from '../../../mainpage/actions/loadingDataAction';
import { updateLanguage } from '../../actions';

export class Language extends PureComponent {
    constructor(props) {
        super(props);

        this.languageDropdown = React.createRef();

        this.state = {
            isChangingLanguage: false
        }
    }

    handleChangeLanguage = async (event) => {
        const isConfirmed = window.confirm("Are you sure you want to change language for this affiliate?");
        if (isConfirmed) {
            const preferredLanguage = event.target.value;
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, PreferredLanguage: preferredLanguage });
            const result = await this.props.updateLanguage(param);

            if (result.IsSuccessful) {
                window.alert("The language has been updated successfully!");
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(result.Message);
            }
        }
    }

    toggleChangingLanguage = (isChanging) => {
        this.setState({ isChangingLanguage: isChanging }, () => isChanging && this.languageDropdown.current.focus());
    }

    render() {
        const { isChangingLanguage } = this.state;
        const { affiliateInfo, languageList } = this.props;

        return (
            <React.Fragment>
                <Input type="select" style={{ display: isChangingLanguage ? 'inline-block' : 'none' }} value={affiliateInfo.PreferredLanguage}
                    onChange={this.handleChangeLanguage} onBlur={() => this.toggleChangingLanguage(false)} ref={this.languageDropdown}>
                    {
                        languageList.map(language => {
                            return (<option value={language.LanguageCode} key={`affinfo-lang-${language.LanguageCode}`}>{language.LanguageName}</option>)
                        })
                    }
                </Input>
                <Button color="link p-0 left-panel-btn-link" style={{ display: isChangingLanguage ? 'none' : 'inline-block' }} onClick={() => this.toggleChangingLanguage(true)}>
                    {
                        languageList.map(language => {
                            return affiliateInfo.PreferredLanguage === language.LanguageCode && language.LanguageName
                        })
                    }
                </Button>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ affCommonInfo, leftPanel, languageList }) {
    const { affiliateInfo } = leftPanel;
    const { affName } = affCommonInfo;
    return Object.assign({}, { affiliateInfo, languageList, affName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, updateLanguage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Language);