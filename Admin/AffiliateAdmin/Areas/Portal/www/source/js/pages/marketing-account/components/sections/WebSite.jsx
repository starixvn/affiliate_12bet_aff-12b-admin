import React, { useState, useRef, useEffect } from 'react';
import { Form, FormGroup, Label, Col, Button, Input } from 'reactstrap';

const description = `Please describe how you would like to promote 12Bet if you don't have the URL`;

const getMoreSiteUrls = (moreSiteUrls) => {
    if(moreSiteUrls) {
        return moreSiteUrls.split(";").map(url => ({
            url: url,
            ref: React.createRef()
        }));
    }
    return [];
}

const createMoreSiteUrl = (url) => ({
    url: url,
    ref: React.createRef()
})

const SiteFieldComponent = ({ label, name, value, onChange, onBlur, disabled, ref }) => {
    return (
        <React.Fragment>
            <Label sm={2} className="d-flex align-items-center">{label}</Label>
            <Col sm={6} className="input-item d-flex align-items-center">
                <Input 
                    type="text" 
                    name={name} 
                    disabled={disabled} 
                    value={value} 
                    onChange={onChange} 
                    onBlur={onBlur} />
            </Col>
        </React.Fragment>
    )
}

const MoreSitesComponents = ({ moreSiteUrls, onChangeMoreSiteUrl, _handleDeleteUrl, disabled, websiteInfo }) => {
    return (
        <React.Fragment>
            {
                moreSiteUrls.map((siteUrl, index) => {
                    return (
                        <FormGroup row key={`siteUrl${index}`} className="pt-1">
                            <SiteFieldComponent 
                                label={`Website URL ${index + 2}`} 
                                name={`siteUrl${index}`} 
                                value={siteUrl.url} 
                                onChange={(e) => onChangeMoreSiteUrl(e, siteUrl.ref)}     
                                ref={siteUrl.ref}
                                disabled={!disabled} />
                            <Button 
                                outline 
                                color="danger" 
                                disabled={!disabled}
                                className="h-100 d-flex align-items-center mt-1"
                                onClick={() => _handleDeleteUrl(index)}>
                                Del
                            </Button>    
                        </FormGroup>
                    )
                })
            }
        </React.Fragment>
    )
}

export const WebSite = (props) => {
    const { websiteInfo, setWebsiteInfo } = props;
    const [ moreSiteUrls, setMoreSiteUrls ] = useState(getMoreSiteUrls(websiteInfo.moreSiteUrls));

    useEffect(() => {
        setWebsiteInfo({
            ...websiteInfo,
            moreSiteUrls: moreSiteUrls.map((siteUrl) => siteUrl.url).join(';')
        });
    }, [moreSiteUrls]);

    const _handleAddUrl = () => {
        setMoreSiteUrls([...moreSiteUrls, createMoreSiteUrl('')]);
    };

    const _handleDeleteUrl = (index) => {
        setMoreSiteUrls(moreSiteUrls.filter((_, i) => i !== index));
    }

    const _handleChangeHasWebSite = (hasWebSite) => {
        setWebsiteInfo({
            ...websiteInfo,
            hasWebSite: hasWebSite
        });
    }

    const onChangeMoreSiteUrl = (e, ref) => {
        const { value } = e.target;
        setMoreSiteUrls(moreSiteUrls.map((siteUrl) => {
            if (siteUrl.ref === ref) {
                return { ...siteUrl, url: value };
            }
            return siteUrl;
        }));
    }

    const onChange = (e) => {
        const { name, value } = e.target;
        setWebsiteInfo({
            ...websiteInfo,
            [name]: value
        });
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Website Information</h6></legend>
            <Form tag="div">
                <FormGroup>
                    <FormGroup check>
                        <Label check>
                            <Input 
                                type="radio"
                                name='hasWebSite'
                                checked={websiteInfo.hasWebSite} 
                                onChange={onChange}
                                />I have my own website
                        </Label>
                    </FormGroup>
                    <FormGroup row className="pt-1">
                        <SiteFieldComponent 
                            label="Website URL" 
                            name="siteUrl" 
                            value={websiteInfo.siteUrl} 
                            onChange={onChange} 
                            onBlur={onChange} 
                            disabled={!websiteInfo.hasWebSite} />
                        <Button 
                            outline 
                            color="secondary" 
                            disabled={!websiteInfo.hasWebSite || !websiteInfo.siteUrl.trim()} 
                            className="my-1" 
                            onClick={_handleAddUrl}>
                            Add Url
                        </Button>
                    </FormGroup>
                </FormGroup>
                <MoreSitesComponents 
                    moreSiteUrls={moreSiteUrls} 
                    onChangeMoreSiteUrl={onChangeMoreSiteUrl} 
                    _handleDeleteUrl={_handleDeleteUrl} 
                    disabled={websiteInfo.hasWebSite} 
                    websiteInfo={websiteInfo} />
                <FormGroup className="pt-3">
                    <FormGroup check>
                        <Label check>
                            <Input 
                                type="radio" 
                                checked={!websiteInfo.hasWebSite} 
                                onChange={() => _handleChangeHasWebSite(false)}/>I don't have a website
                        </Label>
                    </FormGroup>
                    <FormGroup row className="pt-1">
                        <Label htmlFor="noSiteDesc" sm={3}>{description}</Label>
                        <Col sm={9}>
                            <Input
                                key={`noSiteDesc${websiteInfo.hasWebSite}`}
                                type="textarea" 
                                disabled={websiteInfo.hasWebSite} 
                                style={{ height: 70 }} 
                                id="noSiteDesc" 
                                name="noSiteDesc" 
                                value={websiteInfo.noSiteDesc} onChange={onChange} onBlur={onChange} />
                        </Col>
                    </FormGroup>
                </FormGroup>

            </Form>
        </fieldset>
    );
}

export default React.memo(WebSite);