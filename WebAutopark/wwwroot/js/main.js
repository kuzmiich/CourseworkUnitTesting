import {setCookie, deleteCookie, getCookie} from "./cookie_provider.js";

function setCriteriaCookie(time) {
    const criteriaIdName = "criteria";
    const criteriaValue = document.getElementById(criteriaIdName).selectedIndex;

    setCookie(criteriaIdName, criteriaValue, time);
}

function setAscendingCookie(time) {
    const isAscendingIdName = "isAscending";
    const isAscendingValue = document.getElementById(isAscendingIdName).checked;

    setCookie(isAscendingIdName, isAscendingValue, time);
}

function upload() {
    document.getElementById("criteria").selectedIndex = getCookie("criteria") != null ? getCookie("criteria") : 0;

    document.getElementById("isAscending").checked = getCookie("isAscending") != null ? getCookie("isAscending") === "true" : true;
}

async function main() {
    upload();

    const time = new Date(Date.now() + 3600e3);
    document.getElementById("criteria").addEventListener("change", () => {
        setCriteriaCookie(time);
    });
    document.getElementById("isAscending").addEventListener("click", () => {
        setAscendingCookie(time);
    });

}

main();