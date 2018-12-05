# OAuth demo with mvc5

## features

- basic GitHub OAuth demo

## refs

[rfc6749](http://www.rfcreader.com/#rfc6749)

## roles

- Authorize Server
- Resource Server
- Resource Owner
- Client

## Introduction


传统认证模型的弊端

- 获取并存取用户的密码
- 服务端必须支持密码认证
- 授权范围难以控制
- 用户难以收回授权，只能修改密码
- 第三方应用的安全，会威胁到用户的密码和数据


OAuth通过引入抽象的认证层，分离客户端和资源拥有者解决这一问题。
OAuth中，客户端的访问，被资源用户控制，资源服务器使用不同的授权方式。
不是使用用户密码，而是令牌(specific scope, lifetime, and other access attributes)。

## Github

[api v3 documents](https://developer.github.com/v3/)

[api v3 oauth_authorizations](https://developer.github.com/v3/oauth_authorizations/)

[api v3 Authorizing OAuth Apps](https://developer.github.com/apps/building-oauth-apps/authorizing-oauth-apps/)
