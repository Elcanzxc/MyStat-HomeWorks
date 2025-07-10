#pragma once

#include "Admin.h"
#include "User.h"
#include "TestManager.h"
#include "ResultManager.h"

void adminMenu(Admin& admin, TestManager& testManager);
void userMenu(User& user, TestManager& testManager, ResultManager& resultManager);
